using CalendarApp.BLL.ManagerServices.Abstracts;
using CalendarApp.BLL.ManagerServices.Concretes;
using CalendarApp.COMMON.Results;
using CalendarApp.ENTITIES.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalendarApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpPost("AddEvent")]
        public IActionResult AddEvent(EventDto data)
        {
            Result result = _eventService.AddEvent(data).Result;

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("GetListEvent")]
        public IActionResult GetListEvent()
        {
            var result = _eventService.GetListEvent();

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("GetEventById")]
        public IActionResult GetEventById([FromQuery] IdParam param)
        {
            var result = _eventService.GetEventById(param);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("UpdateEvent")]
        public IActionResult UpdateEvent(UpdateEventDto data)
        {
            Result result = _eventService.UpdateEvent(data).Result;

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("DeleteEvent")]
        public IActionResult DeleteEvent(IdParam param)
        {
            Result result = _eventService.DeleteEvent(param).Result;

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
