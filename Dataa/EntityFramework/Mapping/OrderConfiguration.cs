using ManicureDomain.Entities;

namespace Dataa.EntityFramework.Mapping
{
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        public OrderConfiguration()
        {
            HasRequired(x => x.Client).WithMany(x => x.Orders).HasForeignKey(x => x.ClientId);
            HasMany(x => x.Items).WithRequired();
        }
    }
}
