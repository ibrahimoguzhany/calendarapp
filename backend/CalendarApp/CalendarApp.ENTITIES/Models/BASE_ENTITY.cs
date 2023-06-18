using CalendarApp.ENTITIES.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ENTITIES.Models
{
    public class BASE_ENTITY : IEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        [MaxLength(100)]
        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public BASE_ENTITY()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTimeOffset.Now;
        }
    }
}
