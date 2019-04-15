using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Services.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbset;
        public Repository(ApplicationDbContext Db)
        {
            this._context = Db;
            this._dbset = Db.Set<T>();

        }

        public T Add(T entity)
        {
            var result = _dbset.Add(entity).Entity;
            _context.SaveChanges();
            return result;

        }

        public IQueryable<T> GetAll()
        {
            return _dbset;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }

        public T GetById(object id)
        {
            return _dbset.Find(id);
        }

    }
}
