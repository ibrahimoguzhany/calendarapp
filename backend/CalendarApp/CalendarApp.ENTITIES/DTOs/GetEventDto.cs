using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ENTITIES.DTOs
{
    public class GetEventDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public string Title { get; set; }
    }
}
