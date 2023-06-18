using CalendarApp.DAL.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.BLL.ManagerServices.Concretes
{
    public class BaseManager
    {
        protected IUnitOfWork UnitOfWork { get; }
        public BaseManager(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

    }
}
