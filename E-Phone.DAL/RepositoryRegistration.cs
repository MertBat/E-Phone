using E_Phone.Core.IRepositories.BaseRepository;
using E_Phone.DAL.Repositories.BaseRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.DAL
{
    public static class RepositoryRegistration
    {
        public static void RepositoryInjections(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
