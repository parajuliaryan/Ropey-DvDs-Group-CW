using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ropey_DvDs_Group_CW.Models;
using Ropey_DvDs_Group_CW.Models.Identity;
using Ropey_DvDs_Group_CW.Models.ViewModels;
using Ropey_DvDs_Group_CW.Service;

namespace Ropey_DvDs_Group_CW.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        public UserController(IUserService userService,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager
        )
        {
            _userManager = userManager;
            _userService = userService;
            _roleManager = roleManager;
        }
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UpdatePassword model)
        {
            var authUser = _userService.GetUser();
            var user = await _userManager.FindByNameAsync(authUser);
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return View(); //without error
            }
            else
            {
                return View(); //with error
            }
        }

        public IActionResult ViewUsers()
        {
                var users = _userManager.Users.Select(c => new UserDetailsViewModel()
                {
                    Id = c.Id,
                    UserName = c.UserName,
                    Email = c.Email,
                }).ToList();
                return View(users);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            UpdateUserDetails updateUserDetails = new UpdateUserDetails();
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            updateUserDetails.User = user;
            return View(updateUserDetails);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserDetails detailModel)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var result = await _userManager.ChangePasswordAsync(user, detailModel.CurrentPassword, detailModel.NewPassword);
                user.UserName = detailModel.UserName;
                user.Email = detailModel.Email;
                var result2 = _userManager.UpdateAsync(user);

                if (result.Succeeded && result2.IsCompleted)
                {
                    return View(); //without error
                }
                else
                {
                    return View(); //with error
                }

            }

        }

        public async Task<IActionResult> DeleteUser(string? id)
        {
            UpdateUserDetails updateUserDetails = new UpdateUserDetails();
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            updateUserDetails.User = user;

            return View(updateUserDetails);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StoreUser(UserRegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (!await _roleManager.RoleExistsAsync(UserRoles.Assistant))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Assistant));


            if (await _roleManager.RoleExistsAsync(UserRoles.Assistant))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Assistant);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
