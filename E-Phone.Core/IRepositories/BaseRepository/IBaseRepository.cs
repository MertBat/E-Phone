using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.Core.IRepositories.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<bool> CreateAsync(T entity);
        bool Update(T entity);
        bool Delete(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
    }
}
