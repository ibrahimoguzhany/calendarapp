using CalendarApp.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.DAL.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IEventRepository Events { get; }
        IRoleRepository Roles { get; }
        IUserRefreshTokenRepository RefreshTokens { get; }
        IUserRepository Users { get; }
        Task SaveAsync();
    }
}
