using Azure;
using DTO.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Implementation;
using Services.Interfaces;
using static DTO.Enum.Roles;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;


        public AccountController(IAccountService accountService, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromForm] SignUpVM signUp)
        {
            var userExists = await _userManager.FindByEmailAsync(signUp.Email);
            if (userExists != null)
            {
                // Return a 409 Conflict status code with a message indicating that the user already exists.
                // return Conflict(new { message = "User with this email already exists." });
                return StatusCode(StatusCodes.Status500InternalServerError, new Respo<string> { Status = "Error", Message = "User with this email already exists." });
            }
            var result = await _accountService.SignUp(signUp);
            if (result.Succeeded)
            {
                var response = new Respo<bool> { Status = "Success", Data = true, Message = "User registered successfully." };
                return Ok(response);
            }
            else
            {
                var response = new Respo<List<string>> { Status = "Error", Data = new List<string>(), Message = "Failed to register user." };
                // Handle password validation errors.
                foreach (var error in result.Errors)
                {
                    if (error.Code == "PasswordRequiresNonAlphanumeric")
                    {
                        response.Data.Add("Password must contain at least one non-alphanumeric character (e.g., @, $, #).");
                    }
                    if (error.Code == "PasswordRequiresUpper")
                    {
                        response.Data.Add("Password must contain at least one uppercase letter.");
                    }
                    if (error.Code == "PasswordRequiresDigit")
                    {
                        response.Data.Add("Password must contain at least one Digit.");
                    }
                    // Add more conditions to handle other password validation errors.

                    // Handle other general errors if needed.
                    else
                    {
                        response.Data.Add($"An error occurred: {error.Description}");
                    }
                }
                //return BadRequest(response);
                return StatusCode(StatusCodes.Status500InternalServerError, new Respo<string> { Status = "Error", Message = "Could not register new User." });
            }
            // return Unauthorized();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] LoginVM login)
        {
            var result = await _accountService.Login(login);
            if (string.IsNullOrEmpty(result))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new Respo<string> { Status = "Error", Message = "Please Login First." });
                //return Unauthorized();
            }
            return Ok(result);
        }



    }
}
