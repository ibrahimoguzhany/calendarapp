using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ENTITIES.DTOs
{
    public class UserClaim
    {
        public bool IsSuccess { get; set; }
        public UserClaimType Claim { get; set; }
    }
    public class UserClaimType
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
