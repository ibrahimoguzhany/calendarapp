using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ENTITIES.Models
{
    public class ROLE : BASE_ENTITY
    {
        public string Description { get; set; }
        public virtual List<USER> Users { get; set; }
    }
}
