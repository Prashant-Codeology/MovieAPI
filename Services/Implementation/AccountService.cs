using DTO.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static DTO.Enum.Roles;

namespace Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> SignUp(SignUpVM signUp)
        {
            var userExists = await _userManager.FindByEmailAsync(signUp.Email);
            if (userExists != null) 
            {
                return IdentityResult.Failed(new IdentityError { Description = "User with this email already exists." });
            }
            var user = new AppUser()
            {
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                Email = signUp.Email,
                UserName = signUp.Email
            };
            var result = await _userManager.CreateAsync(user, signUp.Password);
            result = await _userManager.AddToRoleAsync(user, UserRole.User.ToString());//Default Role is User
            return result;
        }
        public async Task<string> Login(LoginVM login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
           if(!result.Succeeded)
            {
                return null;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:validIssuer"],
                audience: _configuration["JWT:validAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
