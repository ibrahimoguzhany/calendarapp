using System;

namespace CalendarApp.ENTITIES.DTOs
{
    public class EventDto
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public bool IsReminded { get; set; }
        public int CalendarId { get; set; }
        
        public int RemaningTime { get; set; }
        public bool IsActive { get; set; }
    }
}
