using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
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
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        Address = model.Address,
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        IdentityResult addToRole = await _userManager.AddToRoleAsync(user, "Users");
                        if (addToRole.Succeeded)
                        {
                            return Ok(user);
                        }
                        else
                        {
                            foreach (var error in addToRole.Errors)
                            {
                                return BadRequest(error.Description);
                            }
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            return BadRequest(error.Description);
                        }
                    }
                }

                return BadRequest(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("EditProfile/{userName}")]
        public async Task<IActionResult> EditProfile([FromRoute] string userName, string password, RegisterDTO model)
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
                    bool chackPassword = await _userManager.CheckPasswordAsync(currentUser, password);
                    if (chackPassword == false)
                    {
                        return BadRequest("Sorry, your password is wrong.");
                    }
                    else
                    {

                        currentUser.UserName = model.UserName;
                        currentUser.Email = model.Email;
                        currentUser.FirstName = model.FirstName;
                        currentUser.LastName = model.LastName;
                        currentUser.PhoneNumber = model.PhoneNumber;
                        currentUser.Address = model.Address;
                        currentUser.PasswordHash = _userManager.PasswordHasher.HashPassword(currentUser, model.Password);


                        IdentityResult result = await _userManager.UpdateAsync(currentUser);
                        if (result.Succeeded)
                        {
                            return Ok(currentUser);
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                return BadRequest(error.Description);
                            }
                        }
                    }
                }

                return BadRequest(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpPost("AddNewAdmin"), Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddNewAdmin(RegisterDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        Address = model.Address,
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        IdentityResult addToRole = await _userManager.AddToRoleAsync(user, "Admin");
                        if (addToRole.Succeeded)
                        {
                            return Ok(user);
                        }
                        else
                        {
                            foreach (var error in addToRole.Errors)
                            {
                                return BadRequest(error.Description);
                            }
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            return BadRequest(error.Description);
                        }
                    }
                }

                return BadRequest(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO model)
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
        public async Task<IActionResult> LoginWithPhoneOrEmail(LoginDTO model)
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
