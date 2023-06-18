using CalendarApp.BLL.ManagerServices.Abstracts;
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
    public class EventService : BaseManager, IEventService
    {
        private readonly ITokenService _tokenService;
        public EventService(IUnitOfWork unitOfWork, ITokenService tokenService) : base(unitOfWork)
        {
            _tokenService = tokenService;
        }
        public async Task<Result> AddEvent(EventDto data)
        {
            Result result = new Result(false);
            var _member = _tokenService.GetClaims();
            if (_member.IsSuccess)
            {
                try
                {
                    if (!string.IsNullOrEmpty(data.Title))
                    {
                        if (data.StartDate.HasValue)
                        {
                            if (DateTimeOffset.Now < data.StartDate)
                            {
                                if (data.EndDate.HasValue)
                                {
                                    if(data.StartDate < data.EndDate)
                                    {
                                        EVENT newEvent = new EVENT
                                        {
                                            Description = data.Description,
                                            Title = data.Title,
                                            StartDate = data.StartDate.Value,
                                            EndDate = data.EndDate.Value,
                                            IsReminded = data.IsReminded,
                                            RemaningTime = data.RemaningTime,
                                            CalendarId = data.CalendarId,
                                            UserId = _member.Claim.Id,
                                            IsActive = data.IsActive,
                                            CreatedBy = _member.Claim.UserName
                                        };
                                        UnitOfWork.Events.Add(newEvent);
                                        await UnitOfWork.SaveAsync();
                                        result.Message = "İşlem başarılı";
                                        result.IsSuccess = true;
                                    }
                                    else
                                    {
                                        result.Message = "Bitiş tarihi başlangıç tarihinden ileri bir tarih olmalıdır";
                                    }
                                }
                                else
                                {
                                    result.Message = "Bitiş tarihi giriniz";
                                }
                            }
                            else
                            {
                                result.Message = "Önümüzdeki dönem için plan oluşturulabilir.";
                            }
                        }
                        else
                        {
                            result.Message = "Başlangıç tarihi giriniz";
                        }
                    }
                    else
                    {
                        result.Message = "Başlık alanı giriniz";
                    }
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            else
            {
                result.Message = "Unauthorized";
            }
            return result;
        }
        public IDataResult<List<GetEventDto>> GetListEvent()
        {
            DataResult<List<GetEventDto>> result = new DataResult<List<GetEventDto>>(null, true);
            List<GetEventDto> events = null;
            var _member = _tokenService.GetClaims();
            if (_member.IsSuccess)
            {
                try
                {
                    var getEvents = UnitOfWork.Events.GetActives().Where(w => w.UserId == _member.Claim.Id).ToList();
                    if (getEvents != null && getEvents.Count > 0)
                    {
                        events = new List<GetEventDto>();
                        foreach (var getEvent in getEvents)
                        {
                            events.Add(new GetEventDto()
                            {
                                Id = getEvent.Id,
                                Title = getEvent.Title,
                                StartDate = getEvent.StartDate
                            });
                        }

                        result.Data = events;
                    }
                }
                catch (Exception ex)
                {
                    result = new DataResult<List<GetEventDto>>(null, false, ex.Message);
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Unauthorized";
            }
            return result;
        }
        public IDataResult<GetEventById> GetEventById(IdParam param)
        {
            DataResult<GetEventById> result = new DataResult<GetEventById>(null, true);
            GetEventById eventResult = null;
            var _member = _tokenService.GetClaims();
            if (_member.IsSuccess)
            {
                try
                {
                    var getEvent = UnitOfWork.Events.Find(param.Id);
                    if (getEvent != null && !getEvent.IsDelete)
                    {
                        eventResult = new GetEventById()
                        {
                            Id = getEvent.Id,
                            Description = getEvent.Description,
                            Title = getEvent.Title,
                            StartDate = getEvent.StartDate,
                            EndDate = getEvent.EndDate,
                            IsReminded = getEvent.IsReminded,
                            RemaningTime = getEvent.RemaningTime,
                            CalendarId = getEvent.CalendarId,
                            IsActive = getEvent.IsActive
                        };

                        result.Data = eventResult;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Olay bulunamadı";
                    }
                }
                catch (Exception ex)
                {
                    result = new DataResult<GetEventById>(null, false, ex.Message);
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Unauthorized";
            }
            return result;
        }
        public async Task<Result> UpdateEvent(UpdateEventDto data)
        {
            Result result = new Result(false);
            var _member = _tokenService.GetClaims();
            if (_member.IsSuccess)
            {
                try
                {
                    if (data.Id != Guid.Empty)
                    {
                        if (!string.IsNullOrEmpty(data.Title))
                        {
                            if (data.StartDate.HasValue)
                            {
                                if (data.EndDate.HasValue)
                                {
                                    var getEvent = UnitOfWork.Events.Find(data.Id);
                                    if (getEvent != null)
                                    {

                                        getEvent.Description = data.Description;
                                        getEvent.Title = data.Title;
                                        getEvent.StartDate = data.StartDate.Value;
                                        getEvent.EndDate = data.EndDate.Value;
                                        getEvent.IsReminded = data.IsReminded;
                                        getEvent.RemaningTime = data.RemaningTime;
                                        getEvent.CalendarId = data.CalendarId;
                                        getEvent.IsActive = data.IsActive;
                                        getEvent.UpdatedBy = _member.Claim.UserName;

                                        UnitOfWork.Events.Update(getEvent);
                                        await UnitOfWork.SaveAsync();
                                        result.Message = "İşlem başarılı";
                                        result.IsSuccess = true;
                                    }
                                    else
                                    {
                                        result.Message = "Olay bulunamadı";
                                    }
                                }
                                else
                                {
                                    result.Message = "Bitiş tarihi giriniz";
                                }
                            }
                            else
                            {
                                result.Message = "Başlangıç tarihi giriniz";
                            }
                        }
                        else
                        {
                            result.Message = "Başlık alanı giriniz";
                        }
                    }
                    else
                    {
                        result.Message = "Olay seçiniz";
                    }
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            else
            {
                result.Message = "Unauthorized";
            }
            return result;
        }
        public async Task<Result> DeleteEvent(IdParam param)
        {
            Result result = new Result(false);
            var _member = _tokenService.GetClaims();
            if (_member.IsSuccess)
            {
                try
                {
                    var getEvent = UnitOfWork.Events.Find(param.Id);
                    if (getEvent != null)
                    {
                        getEvent.UpdatedBy = _member.Claim.UserName;
                        UnitOfWork.Events.Delete(getEvent);
                        UnitOfWork.Events.Update(getEvent);
                        await UnitOfWork.SaveAsync();
                        result.Message = "İşlem başarılı";
                        result.IsSuccess = true;
                    }
                    else
                    {
                        result.Message = "Olay bulunamadı";
                    }
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            else
            {
                result.Message = "Unauthorized";
            }
            return result;
        }
    }
}
