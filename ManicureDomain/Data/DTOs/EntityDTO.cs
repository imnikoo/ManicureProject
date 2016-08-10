using System;

namespace ManicureDomain.DTOs
{
    public class EntityDTO
    {
        public EntityDTO()
        {
            IsDeleted = false;
        }

        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
