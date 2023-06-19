using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ENTITIES.Models
{
    public class EVENT : BASE_ENTITY
    {
        public string Title { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset? RemindDate { get; set; }
        public int CalendarId { get; set; }
        public Guid? UserId { get; set; }
        public virtual USER User { get; set; }
    }
}
