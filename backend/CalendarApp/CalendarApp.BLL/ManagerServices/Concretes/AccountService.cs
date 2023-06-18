using CalendarApp.BLL.ManagerServices.Abstracts;
using CalendarApp.COMMON.Helper;
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
    public class AccountService : BaseManager, IAccountService
    {
        public AccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Result> Register(RegisterDto data)
        {
            Result result = new Result(true);

            try
            {
                var validationRules = new List<Func<RegisterDto, (bool isValid, string errorMessage)>>
                {
                    data => (!string.IsNullOrEmpty(data.FirstName), "İsim giriniz"),
                    data => (!string.IsNullOrEmpty(data.LastName), "Soyisim giriniz"),
                    data => (!string.IsNullOrEmpty(data.Email), "Email giriniz"),
                    data => (!string.IsNullOrEmpty(data.Password), "Şifre giriniz"),
                    data => (!string.IsNullOrEmpty(data.UserName), "Kullanıcı adı giriniz"),
                    data => (!string.IsNullOrEmpty(data.Phone), "Telefon numarası giriniz"),
                    data => (!string.IsNullOrEmpty(data.IdentityNumber), "TC Kimlik numarası giriniz")
                };

                foreach (var rule in validationRules)
                {
                    var (isValid, errorMessage) = rule(data);
                    if (!isValid)
                    {
                        return new Result(false, errorMessage);
                    }
                }

                var users = UnitOfWork.Users.GetActives();
                var control = users.Any(w => w.UserName == data.UserName || w.Email == data.Email.Trim().ToLower());

                if (control)
                {
                    return new Result(false, "Bu kullanıcı adı veya email adresine ait kullanıcı bulunmaktadır");
                }

                USER newUser = new USER
                {
                    Email = data.Email.Trim().ToLower(),
                    Password = HelperService.GetPasswordEncode(data.Password.Trim()),
                    IsActive = true,
                    IsDelete = false,
                    CreatedBy = data.Email,
                    LastName = data.LastName,
                    FirstName = data.FirstName,
                    UserName = data.UserName,
                    Phone = data.Phone,
                    IdentityNumber = data.IdentityNumber,
                    Address = data.Address,
                    RoleId = Guid.Parse("8AD2ACD8-0DAC-11EE-BE56-0242AC120002")
                };

                UnitOfWork.Users.Add(newUser);
                await UnitOfWork.SaveAsync();
                result.Message = "İşlem başarılı";
            }
            catch (Exception ex)
            {
                result = new Result(false, ex.Message);
            }

            return result;

        }
        public async Task<IDataResult<User>> Login(LoginDto data)
        {
            User user = null;
            DataResult<User> result = new DataResult<User>(null, true);

            try
            {
                string password = HelperService.GetPasswordEncode(data.Password);
                var getUser = UnitOfWork.Users.Where(w => w.IsActive && !w.IsDelete && (w.UserName == data.UserNameOrEmail || w.Email == data.UserNameOrEmail && w.Password == password)).FirstOrDefault();

                if (getUser != null)
                {
                    user = new User();
                    user.Id = getUser.Id;
                    user.Email = getUser.Email;
                    user.FirstName = getUser.FirstName;
                    user.LastName = getUser.LastName;
                    user.UserName = getUser.UserName;

                    var refreshToken = getUser.RefreshToken;
                    if (refreshToken != null)
                    {
                        getUser.RefreshToken.UpdatedDate = DateTimeOffset.Now;

                        UnitOfWork.RefreshTokens.Update(refreshToken);
                    }
                    else
                    {
                        refreshToken = new USER_REFRESH_TOKEN();
                        refreshToken.UserId = getUser.Id;
                        refreshToken.RefreshToken = Guid.NewGuid();
                        refreshToken.IsActive = true;
                        UnitOfWork.RefreshTokens.Add(refreshToken);
                    }
                    await UnitOfWork.SaveAsync();
                    user.RefreshToken = refreshToken.RefreshToken;
                    result.Data = user;
                    result.Message = "İşlem başarılı";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<IDataResult<User>> GetRefreshLogin(RefreshTokenDto data)
        {
            User user = null;
            DataResult<User> result = new DataResult<User>(null, true);
            try
            {
                USER_REFRESH_TOKEN refreshToken = UnitOfWork.RefreshTokens.Where(w => w.RefreshToken == data.RefreshToken).FirstOrDefault();
                if (refreshToken != null)
                {
                    var getUser = refreshToken.User;
                    if (getUser != null)
                    {
                        Guid newRefreshToken = Guid.NewGuid(); // Creates a new refresh token.

                        user = new User();
                        user.Id = getUser.Id;
                        user.Email = getUser.Email;
                        user.FirstName = getUser.FirstName;
                        user.LastName = getUser.LastName;
                        user.UserName = getUser.UserName;
                        user.RefreshToken = newRefreshToken;

                        refreshToken.RefreshToken = newRefreshToken;
                        UnitOfWork.RefreshTokens.Update(refreshToken);
                        await UnitOfWork.SaveAsync();

                        result.Data = user;
                        result.Message = "İşlem başarılı";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Kullanıcı bulunamadı";
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}