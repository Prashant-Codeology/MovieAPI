using DAL.DBContext;
using DAL.Repository.Implementation;
using DAL.Repository.Interfaces;
using DAL.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALDependencyInjections
{
    public static class DALDependencyInjections
    {
        //This is Extension method 
        public static IServiceCollection DALServices(this IServiceCollection services, IConfiguration configuration)
        {
            var ConnectionString = configuration.GetConnectionString("DefaultConnection");
           
            services.AddDbContext<MovieDbContext>(options =>
                    options.UseSqlServer(
                        ConnectionString,
                        a => { a.MigrationsAssembly("DAL"); })); // To set which project to put migrations

           
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IRole, Role>();

            return services;
        }
    }
}
