using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataa.EntityFramework.Mapping
{
    public class EntityConfiguration<T> : EntityTypeConfiguration<T> where T : Entity
    {
        public EntityConfiguration()
        {
            Map(m => m.Requires("IsDeleted").HasValue(false)).Ignore(m => m.IsDeleted);
            HasKey(e => e.Id);
        }
    }
}
