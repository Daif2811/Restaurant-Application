using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }



        [HttpGet("Roles")]
        public IActionResult Roles()
        {
            try
            {
                var roles = _roleManager.Roles.ToList();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest("You can not save empty name");
                }
                else
                {
                    IdentityRole role = new IdentityRole()
                    {
                        Name = name
                    };
                    IdentityResult result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return Ok(role);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            return BadRequest(error.Description);
                        }
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpPut("EditRole/{name:alpha}")]
        public async Task<IActionResult> EditRole([FromRoute] string name, [FromBody] string newName)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest("Sorry, Add Role Name for edit it");
                }

                IdentityRole oldRole = await _roleManager.FindByNameAsync(name);
                if (oldRole != null)
                {

                    oldRole.Name = !string.IsNullOrEmpty(newName)?  newName : oldRole.Name;

                    IdentityResult result = await _roleManager.UpdateAsync(oldRole);
                    if (result.Succeeded)
                    {
                        return Ok(oldRole);

                    }
                    else
                    {
                        return BadRequest();
                    }

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




        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromBody] string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest("Sorry, Add Role Name for delete it");
                }

                IdentityRole role = await _roleManager.FindByNameAsync(name);
                if (role != null)
                {
                    await _roleManager.DeleteAsync(role);
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
