using CalendarApp.DAL.Context;
using CalendarApp.DAL.Repositories.Abstracts;
using CalendarApp.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.DAL.Repositories.Concretes
{
    public class UserRefreshTokenRepository : BaseRepository<USER_REFRESH_TOKEN>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(MyContext db) : base(db)
        {
        }
    }
}
