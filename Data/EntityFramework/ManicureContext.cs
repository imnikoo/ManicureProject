﻿using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;

namespace Data.EntityFramework
{
    public class ManicureContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PurchasePlace> PurchasePlaces { get; set; }

        public ManicureContext() : base()
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;

            var deletedEntries = from e in context.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted)
                                 where
                                     e.IsRelationship == false &&
                                     e.Entity != null &&
                                     typeof(Entity).IsAssignableFrom(e.Entity.GetType())
                                 select e;

            foreach (var entry in ChangeTracker.Entries()
                  .Where(p => p.State == EntityState.Deleted).ToList())
                SoftDelete(entry);

            return base.SaveChanges();
        }

        private void SoftDelete(DbEntityEntry entry)
        {
            Type entryEntityType = entry.Entity.GetType();

            string tableName = GetTableName(entryEntityType);
            string primaryKeyName = GetPrimaryKeyName(entryEntityType);

            string deletequery =
                string.Format(
                    "UPDATE {0} SET IsDeleted = 1 WHERE {1} = @id",
                        tableName, primaryKeyName);

            Database.ExecuteSqlCommand(
                deletequery,
                new SqlParameter("@id", entry.OriginalValues[primaryKeyName]));

            //Marking it Unchanged prevents the hard delete
            //entry.State = EntityState.Unchanged;
            //So does setting it to Detached:
            //And that is what EF does when it deletes an item
            //http://msdn.microsoft.com/en-us/data/jj592676.aspx
            entry.State = EntityState.Detached;
        }

        private EntitySetBase GetEntitySet(Type type)
        {
            if (!_mappingCache.ContainsKey(type))
            {
                ObjectContext octx = ((IObjectContextAdapter)this).ObjectContext;

                string typeName = ObjectContext.GetObjectType(type).Name;

                var es = octx.MetadataWorkspace
                                .GetItemCollection(DataSpace.SSpace)
                                .GetItems<EntityContainer>()
                                .SelectMany(c => c.BaseEntitySets
                                               .Where(e => e.Name == typeName))
                                .FirstOrDefault();

                if (es == null)
                    throw new ArgumentException("Entity type not found in GetTableName", typeName);

                _mappingCache.Add(type, es);
            }

            return _mappingCache[type];
        }

        private string GetTableName(Type type)
        {
            EntitySetBase es = GetEntitySet(type);

            return string.Format("[{0}].[{1}]",
                es.MetadataProperties["Schema"].Value,
                es.MetadataProperties["Table"].Value);
        }

        private string GetPrimaryKeyName(Type type)
        {
            EntitySetBase es = GetEntitySet(type);

            return es.ElementType.KeyMembers[0].Name;
        }

        private static Dictionary<Type, EntitySetBase> _mappingCache =
       new Dictionary<Type, EntitySetBase>();
    }
}
