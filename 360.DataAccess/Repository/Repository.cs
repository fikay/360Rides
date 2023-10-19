using _360.DataAccess.Data;
using _360.DataAccess.Repository.Irepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _360.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public void Add(T item)
        {
            this.dbSet.Add(item);   
        }

        public void Delete(T item)
        {
            dbSet.Remove(item);
        }

        public T Get(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> queryable;
            if(tracked)
            {
                queryable = dbSet;   
            }
            else
            {
                queryable = dbSet.AsNoTracking();
            }

           queryable =  queryable.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(property);
                }
            }

            return queryable.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> queryable = dbSet;
            if (filter != null)
            {
                queryable.Where(filter);
            }
            
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(property);
                }
            }

            return queryable.ToList();
        }
    }
}
