using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.DBContext
{
    public class MovieDbContext : IdentityDbContext<AppUser>
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
    }
}
