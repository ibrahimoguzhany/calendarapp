using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ENTITIES.Models
{
    public class USER_ROLE:BASE_ENTITY
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public virtual USER User { get; set; }
        public virtual ROLE Role { get; set; }
    }
}
