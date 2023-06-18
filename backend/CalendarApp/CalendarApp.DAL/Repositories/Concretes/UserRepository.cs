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
    public class UserRepository : BaseRepository<USER>, IUserRepository
    {
        public UserRepository(MyContext db) : base(db)
        {
        }
    }
}
