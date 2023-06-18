using CalendarApp.COMMON.Results;
using CalendarApp.ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.BLL.ManagerServices.Abstracts
{
    public interface IUserService
    {
        IDataResult<AuthDto> GetAuth();
        Task<Result> UpdateUser(UpdateUserDto data);
        IDataResult<GetUserDto> GetUser();
        Task<Result> UpdatePassword(PasswordDto data);
    }
}
