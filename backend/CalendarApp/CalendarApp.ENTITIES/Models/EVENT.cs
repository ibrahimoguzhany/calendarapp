using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ENTITIES.Models
{
    public class EVENT : BASE_ENTITY
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsReminded { get; set; }
        public int RemaningTime { get; set; }
        public Guid? UserId { get; set; }
        public virtual USER User { get; set; }
    }
}
