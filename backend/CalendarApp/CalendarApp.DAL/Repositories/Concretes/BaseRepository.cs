using CalendarApp.DAL.Context;
using CalendarApp.DAL.Repositories.Abstracts;
using CalendarApp.ENTITIES.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.DAL.Repositories.Concretes
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly MyContext _db;

        public BaseRepository(MyContext db)
        {
            _db = db;
        }

        public void Add(T item)
        {
            _db.Set<T>().Add(item);
        }

        public void AddRange(List<T> list)
        {
            _db.Set<T>().AddRange(list);
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().Any(exp);
        }

        public void Delete(T item)
        {
            item.UpdatedDate = DateTime.Now;
            item.IsDelete = true;
        }

        public void DeleteRange(List<T> list)
        {
            foreach (T item in list)
            {
                Delete(item);
            }
        }

        public void Destroy(T item)
        {
            _db.Set<T>().Remove(item);
        }

        public void DestroyRange(List<T> list)
        {
            _db.Set<T>().RemoveRange(list);
        }

        public T Find(Guid id)
        {
            return _db.Set<T>().Find(id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().FirstOrDefault(exp);
        }

        public List<T> GetActives()
        {
            return Where(x => x.IsActive && !x.IsDelete);
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetFirstData()
        {
            return _db.Set<T>().OrderBy(x => x.CreatedDate).FirstOrDefault();
        }

        public T GetLastData()
        {
            return _db.Set<T>().OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        }

        public List<T> GetPassives()
        {
            return Where(x => !x.IsActive);
        }

        public object Select(Expression<Func<T, object>> exp)
        {
            return _db.Set<T>().Select(exp).ToList();

        }

        public IQueryable<X> SelectViaClass<X>(Expression<Func<T, X>> exp)
        {
            return _db.Set<T>().Select(exp);

        }

        public void Update(T item)
        {
            item.UpdatedDate = DateTime.Now;
            T toBeUpdated = Find(item.Id);
            _db.Entry(toBeUpdated).CurrentValues.SetValues(item);
        }

        public void UpdateRange(List<T> list)
        {
            foreach (T item in list)
            {
                Update(item);
            }
        }

        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().Where(exp).ToList();
        }
    }
}
