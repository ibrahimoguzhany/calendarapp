using CalendarApp.DAL.Context;
using CalendarApp.DAL.Repositories.Abstracts;
using CalendarApp.DAL.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _db;
        private EventRepository _eventRepository;
        private RoleRepository _roleRepository;
        private UserRefreshTokenRepository _refreshTokenRepository;
        private UserRepository _userRepository;
        public UnitOfWork(MyContext db)
        {
            _db = db;
        }

        public IEventRepository Events => _eventRepository ??= new EventRepository(_db);

        public IRoleRepository Roles => _roleRepository ??= new RoleRepository(_db);

        public IUserRefreshTokenRepository RefreshTokens => _refreshTokenRepository ??= new UserRefreshTokenRepository(_db);

        public IUserRepository Users => _userRepository ??= new UserRepository(_db);

        public async ValueTask DisposeAsync()
        {
            await _db.DisposeAsync();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
