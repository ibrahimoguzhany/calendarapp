using CalendarApp.COMMON.Results;
using CalendarApp.ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.BLL.ManagerServices.Abstracts
{
    public interface IRoleService
    {
        Task<Result> AddRole(RoleDto data);
        IDataResult<List<GetRoleDto>> GetListRole();
    }
}
