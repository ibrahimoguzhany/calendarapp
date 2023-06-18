using CalendarApp.BLL.ManagerServices.Abstracts;
using CalendarApp.BLL.ManagerServices.Concretes;
using CalendarApp.COMMON.Results;
using CalendarApp.DAL.UnitOfWorks;
using CalendarApp.ENTITIES.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace CalendarApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAuth")]
        public IActionResult GetAuth()
        {
            var result = _userService.GetAuth();

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser(UpdateUserDto data)
        {
            var result = _userService.UpdateUser(data).Result;

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("UpdatePassword")]
        public IActionResult UpdatePassword(PasswordDto data)
        {
            var result = _userService.UpdatePassword(data).Result;

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            var result = _userService.GetUser();

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
