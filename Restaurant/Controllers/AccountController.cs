using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.IRepository;
using Restaurant.Models;
using Restaurant.Models.DTO;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
           
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._mapper = mapper;
        }



        [HttpGet("Users")]
        public IActionResult Users()
        {
            try
            {
                List<ApplicationUser> users = _userManager.Users.ToList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(errors);
                }


                ApplicationUser existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    return BadRequest("User with this email already exists.");
                }


                ApplicationUser user = _mapper.Map<ApplicationUser>(model);

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);


                    if (!await _roleManager.RoleExistsAsync("Users"))
                    {
                        // Role doesn't exist, create it
                        var roleResult = await _roleManager.CreateAsync(new IdentityRole("Users"));

                        if (!roleResult.Succeeded)
                        {
                            var roleErrors = roleResult.Errors.Select(error => error.Description).ToList();
                            return BadRequest(roleErrors);
                        }
                    }

                    IdentityResult addToRole = await _userManager.AddToRoleAsync(user, "Users");
                    if (addToRole.Succeeded)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        var errors = addToRole.Errors.Select(error => error.Description).ToList();
                        return BadRequest(errors);
                    }
                }
                else
                {
                    var errors = result.Errors.Select(error => error.Description).ToList();
                    return BadRequest(errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpPost("EditProfile/{userName}")]
        public async Task<IActionResult> EditProfile([FromRoute] string userName, string password, EditProfileDto model)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    return BadRequest("Sorry, Enter User Name to Edit your profile.");
                }
                if (string.IsNullOrEmpty(password))
                {
                    return BadRequest("Sorry, Enter your password to Edit your profile.");
                }

                ApplicationUser currentUser = await _userManager.FindByNameAsync(userName);

                if (currentUser != null)
                {
                    //This line will map the properties from (( model to currentUser )).
                    _mapper.Map(model, currentUser);

                    bool checkPassword = await _userManager.CheckPasswordAsync(currentUser, password);

                    if (checkPassword == false)
                    {
                        return BadRequest("Sorry, your password is wrong.");
                    }

                    // currentUser.PasswordHash = _userManager.PasswordHasher.HashPassword(currentUser, model.Password);
                    IdentityResult passwordChangeResult = await _userManager.ChangePasswordAsync(currentUser, password, model.Password);

                    IdentityResult result = await _userManager.UpdateAsync(currentUser);
                    if (result.Succeeded)
                    {
                        return Ok(currentUser);
                    }
                    else
                    {
                        var errors = result.Errors.Select(error => error.Description).ToList();
                        return BadRequest(errors);
                    }

                }
                return BadRequest("User not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpPost("AddNewAdmin"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewAdmin(RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(errors);
                }


                ApplicationUser existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    return BadRequest("User with this email already exists.");
                }


                ApplicationUser user = _mapper.Map<ApplicationUser>(model);

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);

                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        // Role doesn't exist, create it
                        var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));

                        if (!roleResult.Succeeded)
                        {
                            var roleErrors = roleResult.Errors.Select(error => error.Description).ToList();
                            return BadRequest(roleErrors);
                        }
                    }

                    IdentityResult addToRole = await _userManager.AddToRoleAsync(user, "Admin");
                    if (addToRole.Succeeded)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        var errors = addToRole.Errors.Select(error => error.Description).ToList();
                        return BadRequest(errors);
                    }
                }
                else
                {
                    var errors = result.Errors.Select(error => error.Description).ToList();
                    return BadRequest(errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
                    if (user != null)
                    {
                        bool UserPass = await _userManager.CheckPasswordAsync(user, model.Password);
                        if (UserPass)
                        {
                            await _signInManager.SignInAsync(user, model.RememberMe);
                            return Ok(user);
                        }
                        else
                        {
                            return BadRequest("Sorry, Password in Wrong");
                        }
                    }
                    return BadRequest("Sorry, User Name is wrong");
                }
                return BadRequest(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("LoginWithPhoneOrEmail")]
        public async Task<IActionResult> LoginWithPhoneOrEmail(LoginDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
                    if (user != null)
                    {
                        bool UserPass = await _userManager.CheckPasswordAsync(user, model.Password);
                        if (UserPass)
                        {
                            await _signInManager.SignInAsync(user, model.RememberMe);
                            return Ok(user);
                        }
                        else
                        {
                            return BadRequest("Sorry, Password in Wrong");
                        }
                    }
                    return BadRequest("Sorry, User Name is wrong");
                }
                return BadRequest(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            try
            {
                _signInManager.SignOutAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    return BadRequest("Sorry, Add User Name for delete it");
                }

                ApplicationUser user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





    }
}
