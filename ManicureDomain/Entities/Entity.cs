using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Entities
{
    public class Entity
    {
        public Entity()
        {
            IsActive = true;
        }
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
