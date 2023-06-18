using CalendarApp.COMMON.Results;
using CalendarApp.ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.BLL.ManagerServices.Abstracts
{
    public interface IAccountService
    {
        Task<Result> Register(RegisterDto data);
        Task<IDataResult<User>> Login(LoginDto data);
        Task<IDataResult<User>> GetRefreshLogin(RefreshTokenDto data);
    }
}
