using CalendarApp.BLL.ManagerServices.Abstracts;
using CalendarApp.BLL.ManagerServices.Concretes;
using CalendarApp.DAL.Repositories.Abstracts;
using CalendarApp.DAL.Repositories.Concretes;
using CalendarApp.DAL.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.BLL.ServiceInjection
{
    public static class RepManService
    {
        public static IServiceCollection AddRepManServices(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEventService, EventService>();
            return services;
        }
    }
}
