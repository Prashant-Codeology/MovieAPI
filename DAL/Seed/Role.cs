using DAL.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO.Enum.Roles;

namespace DAL.Seed
{
    public class Role : IRole
    {
        private readonly MovieDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Role(MovieDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (!_roleManager.RoleExistsAsync(UserRole.Admin.ToString()).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString())).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UserRole.User.ToString())).GetAwaiter().GetResult();
            }

            var user = new AppUser
            {
                //Id=Guid.NewGuid().ToString(),
                UserName = "prashantsubedi11@gmail.com",
                NormalizedUserName = "PRASHANTSUBEDI11@GMAIL.COM",
                Email = "prashantsubedi11@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "PRASHANTSUBEDI11@GMAIL.COM",
                LockoutEnabled = true,
                PhoneNumber = "9843807461",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var userManager = _userManager.CreateAsync(user, "Password@123").GetAwaiter().GetResult();

            var result = _context.Users.FirstOrDefault(u => u.Email == "prashantsubedi11@gmail.com") ?? throw new NullReferenceException();

            _userManager.AddToRoleAsync(result, UserRole.Admin.ToString()).GetAwaiter().GetResult();

            await _context.SaveChangesAsync();
        }
    }
}
