using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ENTITIES.DTOs
{
    public class UpdateEventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int CalendarId { get; set; }
        public DateTimeOffset? RemindDate { get; set; }
    }
}
