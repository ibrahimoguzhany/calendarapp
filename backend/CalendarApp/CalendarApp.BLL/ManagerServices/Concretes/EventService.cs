using CalendarApp.BLL.ManagerServices.Abstracts;
using CalendarApp.COMMON.Results;
using CalendarApp.DAL.UnitOfWorks;
using CalendarApp.ENTITIES.DTOs;
using CalendarApp.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<DataResult<Guid>> AddEvent(EventDto data)
        {
            DataResult<Guid> result = new DataResult<Guid>(Guid.Empty, true);
            var _member = _tokenService.GetClaims();
            if (!_member.IsSuccess)
            {
                result.Message = "Unauthorized";
                return result;
            }

            try
            {
                ValidateData(data, result);
                if (!result.IsSuccess)
                {
                    return result;
                }

                EVENT newEvent = new EVENT
                {
                    Title = data.Title,
                    StartDate = data.StartDate.Value,
                    EndDate = data.EndDate.Value,
                    RemindDate = data.RemindDate.HasValue ? data.RemindDate.Value : null,
                    CalendarId = data.CalendarId,
                    UserId = _member.Claim.Id,
                    IsActive = true,
                    CreatedBy = _member.Claim.UserName
                };
                UnitOfWork.Events.Add(newEvent);
                await UnitOfWork.SaveAsync();

                result.Message = "İşlem başarılı";
                result.IsSuccess = true;
                result.Data = newEvent.Id;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
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
                                CalendarId = getEvent.CalendarId,
                                StartDate = getEvent.StartDate,
                                EndDate = getEvent.EndDate,
                                RemindDate = getEvent.RemindDate,
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
                            Title = getEvent.Title,
                            StartDate = getEvent.StartDate,
                            EndDate = getEvent.EndDate,
                            RemindDate = getEvent.RemindDate,
                            CalendarId = getEvent.CalendarId,
                            IsActive = true
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
            if (!_member.IsSuccess)
            {
                result.Message = "Unauthorized";
                return result;
            }

            try
            {
                ValidateUpdateData(data, result);
                if (!result.IsSuccess)
                {
                    return result;
                }

                var getEvent = UnitOfWork.Events.Find(data.Id);

                if (getEvent != null)
                {
                    getEvent.Title = data.Title;
                    getEvent.StartDate = data.StartDate.Value;
                    getEvent.EndDate = data.EndDate.Value;
                    if (data.RemindDate != null)
                    {
                        getEvent.RemindDate = data.RemindDate.Value;
                    }
                    getEvent.CalendarId = data.CalendarId;
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
            catch (Exception ex)
            {
                result.Message = ex.Message;
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

        #region Private Methods
        private void ValidateData(EventDto data, Result result)
        {
            var validationList = new List<Func<string>>
            {
                () => string.IsNullOrEmpty(data.Title) ? "Başlık alanı giriniz" : null,
                () => !data.StartDate.HasValue ? "Başlangıç tarihi giriniz" : null,
                () => !data.EndDate.HasValue ? "Bitiş tarihi giriniz" : null,
                () => data.StartDate >= data.EndDate ? "Bitiş tarihi başlangıç tarihinden ileri bir tarih olmalıdır" : null
            };

            foreach (var validation in validationList)
            {
                var message = validation.Invoke();
                if (message != null)
                {
                    result.Message = message;
                    return;
                }
            }

            result.IsSuccess = true;
        }
        private void ValidateUpdateData(UpdateEventDto data, Result result)
        {
            var validationList = new List<Func<string>>
            {
                () => data.Id == Guid.Empty ? "Olay seçiniz" : null,
                () => !data.StartDate.HasValue ? "Başlangıç tarihi giriniz" : null,
                () => !data.EndDate.HasValue ? "Bitiş tarihi giriniz" : null,
            };

            foreach (var validation in validationList)
            {
                var message = validation.Invoke();
                if (message != null)
                {
                    result.Message = message;
                    return;
                }
            }

            result.IsSuccess = true;
        }
        #endregion

    }
}
