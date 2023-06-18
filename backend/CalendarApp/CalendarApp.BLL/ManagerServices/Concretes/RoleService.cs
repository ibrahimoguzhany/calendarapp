using CalendarApp.BLL.ManagerServices.Abstracts;
using CalendarApp.COMMON.Helper;
using CalendarApp.COMMON.Results;
using CalendarApp.DAL.UnitOfWorks;
using CalendarApp.ENTITIES.DTOs;
using CalendarApp.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.BLL.ManagerServices.Concretes
{
    public class RoleService : BaseManager, IRoleService
    {
        private readonly ITokenService _tokenService;
        public RoleService(IUnitOfWork unitOfWork, ITokenService tokenService) : base(unitOfWork)
        {
            _tokenService = tokenService;
        }
        public async Task<Result> AddRole(RoleDto data)
        {
            Result result = new Result(true);
            try
            {
                var roles = UnitOfWork.Roles.GetActives();
                var control = roles.Where(w => w.Description == data.Description).Any();
                if (!control)
                {
                    ROLE newRole = new ROLE();
                    newRole.CreatedBy = "system";
                    newRole.Description = data.Description;
                    newRole.IsActive = true;

                    UnitOfWork.Roles.Add(newRole);
                    await UnitOfWork.SaveAsync();
                    result.Message = "İşlem başarılı";
                }
                else
                {
                    result = new Result(false, "Bu rol bulunmaktadır");
                }
            }
            catch (Exception ex)
            {
                result = new Result(false, ex.Message);
            }
            return result;
        }
        public IDataResult<List<GetRoleDto>> GetListRole()
        {
            DataResult<List<GetRoleDto>> result = new DataResult<List<GetRoleDto>>(null, true);
            List<GetRoleDto> roles = null;
            try
            {
                var getRoles = UnitOfWork.Roles.GetActives();
                if (getRoles != null && getRoles.Count > 0)
                {
                    roles = new List<GetRoleDto>();
                    foreach (var role in getRoles)
                    {
                        roles.Add(new GetRoleDto()
                        {
                            Id = role.Id,
                            Description = role.Description
                        });
                    }

                    result.Data = roles;
                }
            }
            catch (Exception ex)
            {
                result = new DataResult<List<GetRoleDto>>(null, false, ex.Message);
            }
            return result;
        }
    }
}
