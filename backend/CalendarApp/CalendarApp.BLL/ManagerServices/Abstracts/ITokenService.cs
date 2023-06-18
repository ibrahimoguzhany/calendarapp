using CalendarApp.ENTITIES.DTOs;
using CalendarApp.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.BLL.ManagerServices.Abstracts
{
    public interface ITokenService
    {
        string GenerateJWT(User userInfo);
        UserClaim GetClaims();
    }
}
