using CalendarApp.ENTITIES.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ENTITIES.Models
{
    public class USER_REFRESH_TOKEN : BASE_ENTITY
    {
        public Guid RefreshToken { get; set; }
        public Guid? UserId { get; set; }
        public virtual USER User { get; set; }
    }
}
