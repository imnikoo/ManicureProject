using System;

namespace ManicureDomain.Entities
{
    public class Entity
    {
        public Entity()
        {
            IsDeleted = false;
        }

        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
