﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ENTITIES.Models
{
    public class USER : BASE_ENTITY
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        public Guid? RoleId { get; set; }
        public virtual ROLE Role { get; set; }
        public virtual List<EVENT> Events { get; set; }
        public virtual USER_REFRESH_TOKEN RefreshToken { get; set; }
    }
}