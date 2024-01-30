using E_Phone.Core.IRepositories.BaseRepository;
using E_Phone.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.DAL.Repositories.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> _set;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _set.Update(entity);
             _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression) => await _set.Where(expression).FirstOrDefaultAsync();

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            if(expression == null)
                return _set.ToList();
           return await _set.Where(expression).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            T entity = await _set.Where(expression).FirstOrDefaultAsync();

            return entity != null;
        }
    }
}
