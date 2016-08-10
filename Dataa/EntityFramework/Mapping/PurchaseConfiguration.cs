using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataa.EntityFramework.Mapping
{
    public class PurchaseConfiguration : EntityConfiguration<Purchase>
    {
        public PurchaseConfiguration()
        {
            HasRequired(x => x.PurchasePlace).WithMany().HasForeignKey(x => x.PurchasePlaceId);
            HasRequired(x => x.Item).WithMany().HasForeignKey(x => x.ItemId);
        }
    }
}
