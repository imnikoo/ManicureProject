using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataa.EntityFramework.Mapping
{
    public class ItemConfiguration : EntityConfiguration<Item>
    {
        public ItemConfiguration()
        {
            HasMany(x => x.Purchases).WithRequired(x => x.Item).HasForeignKey(x => x.ItemId);
            HasRequired(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
        }
    }
}
