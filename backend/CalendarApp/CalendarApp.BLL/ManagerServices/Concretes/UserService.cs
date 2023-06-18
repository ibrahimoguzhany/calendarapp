using CalendarApp.BLL.ManagerServices.Abstracts;
using CalendarApp.COMMON.Helper;
using CalendarApp.COMMON.Results;
using CalendarApp.DAL.UnitOfWorks;
using CalendarApp.ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.BLL.ManagerServices.Concretes
{
    internal class UserService : BaseManager, IUserService
    {
        private readonly ITokenService _tokenService;
        public UserService(IUnitOfWork unitOfWork, ITokenService tokenService) : base(unitOfWork)
        {
            _tokenService = tokenService;
        }
        public IDataResult<AuthDto> GetAuth()
        {
            DataResult<AuthDto> result = new DataResult<AuthDto>(null, true);
            AuthDto authDto = null;
            var _member = _tokenService.GetClaims();
            if (_member.IsSuccess)
            {
                try
                {
                    authDto = new AuthDto();
                    authDto.Name = _member.Claim.FullName;
                    authDto.Email = _member.Claim.Email;

                    result.Data = authDto;
                    result.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    result = new DataResult<AuthDto>(null, false, ex.Message);
                }
            }
            else
            {
                result.Data = null;
                result.IsSuccess = false;
                result.Message = "Unauthorized";
            }
            return result;
        }
        public async Task<Result> UpdateUser(UpdateUserDto data)
        {
            Result result = new Result(false);
            var _member = _tokenService.GetClaims();
            if (_member.IsSuccess)
            {
                try
                {
                    if (!string.IsNullOrEmpty(data.FirstName))
                    {
                        if (!string.IsNullOrEmpty(data.LastName))
                        {
                            var getUser = UnitOfWork.Users.Find(_member.Claim.Id);
                            getUser.FirstName = data.FirstName;
                            getUser.LastName = data.LastName;
                            getUser.Address = data.Address;

                            UnitOfWork.Users.Update(getUser);
                            await UnitOfWork.SaveAsync();
                            result.IsSuccess = true;
                            result.Message = "İşlem başarılı";
                        }
                        else
                        {
                            result.Message = "Soyisim giriniz";
                        }
                    }
                    else
                    {
                        result.Message = "İsim giriniz";
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
        public async Task<Result> UpdatePassword(PasswordDto data)
        {
            Result result = new Result(false);
            var _member = _tokenService.GetClaims();
            if (_member.IsSuccess)
            {
                try
                {
                    if (!string.IsNullOrEmpty(data.OldPassword))
                    {
                        if (!string.IsNullOrEmpty(data.NewPassword))
                        {
                            var getUser = UnitOfWork.Users.Find(_member.Claim.Id);
                            if (getUser != null)
                            {
                                string oldPassword = HelperService.GetPasswordEncode(data.OldPassword.Trim());
                                if (getUser.Password == oldPassword)
                                {
                                    getUser.Password = HelperService.GetPasswordEncode(data.NewPassword.Trim());
                                    getUser.UpdatedBy = _member.Claim.Email;
                                    UnitOfWork.Users.Update(getUser);
                                    await UnitOfWork.SaveAsync();
                                    result.IsSuccess = true;
                                    result.Message = "İşlem başarılı";
                                }
                                else
                                {
                                    result.Message = "Yeni şifreniz ile şifre tekrarınız birbiri ile uyumsuzdur";
                                }
                            }
                            else
                            {
                                result.Message = "Kullanıcı bulunamadı";
                            }
                        }
                        else
                        {
                            result.Message = "Yeni şifre giriniz";
                        }
                    }
                    else
                    {
                        result.Message = "Eski şifre giriniz";
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
        public IDataResult<GetUserDto> GetUser()
        {
            DataResult<GetUserDto> result = new DataResult<GetUserDto>(null, true);
            GetUserDto user = null;
            var _member = _tokenService.GetClaims();
            if (_member.IsSuccess)
            {
                try
                {
                    var getUser = UnitOfWork.Users.Find(_member.Claim.Id);
                    if (getUser != null)
                    {
                        user = new GetUserDto();
                        user.Id = getUser.Id;
                        user.FirstName = getUser.FirstName;
                        user.LastName = getUser.LastName;
                        user.UserName = getUser.UserName;
                        user.Email = getUser.Email;
                        user.Phone = getUser.Phone;
                        user.IdentityNumber = getUser.IdentityNumber;
                        user.Address = getUser.Address;

                        result.Data = user;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Kullanıcı bulunamadı";
                    }
                }
                catch (Exception ex)
                {
                    result = new DataResult<GetUserDto>(null, false, ex.Message);
                }
            }
            else
            {
                result.Data = null;
                result.IsSuccess = false;
                result.Message = "Unauthorized";
            }
            return result;
        }
    }
}
