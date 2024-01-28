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

        public async Task<bool> CreateAsync(T entity)
        {
            await _set.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public bool Update(T entity)
        {
            _set.Update(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            T entity = _set.Find(id);

            if (entity == null)
                throw new ArgumentException("Marka bulunamadı");

            _set.Remove(entity);
            return _context.SaveChanges() > 0;
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
