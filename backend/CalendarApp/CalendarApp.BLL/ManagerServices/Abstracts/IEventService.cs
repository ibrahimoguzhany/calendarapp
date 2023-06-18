using CalendarApp.COMMON.Results;
using CalendarApp.ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.BLL.ManagerServices.Abstracts
{
    public interface IEventService
    {
        Task<Result> AddEvent(EventDto data);
        IDataResult<List<GetEventDto>> GetListEvent();
        IDataResult<GetEventById> GetEventById(IdParam param);
        Task<Result> UpdateEvent(UpdateEventDto data);
        Task<Result> DeleteEvent(IdParam param);
    }
}
