using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNet.Models;

namespace SocialNet.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);
            var model = new UserViewModel(result);
            return View("User", model);
        }
    }
}
