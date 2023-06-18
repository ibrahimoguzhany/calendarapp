using CalendarApp.BLL.ManagerServices.Abstracts;
using CalendarApp.COMMON.Results;
using CalendarApp.ENTITIES.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalendarApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("AddRole")]
        public IActionResult AddRole(RoleDto data)
        {
            Result result = _roleService.AddRole(data).Result;

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("GetListRole")]
        public IActionResult GetListRole()
        {
            var result = _roleService.GetListRole();

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
