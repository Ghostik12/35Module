using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNet.Extentions;
using SocialNet.Models;
using SocialNet.Repository;

namespace SocialNet.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public UserController(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Route("MyPage")]
        [HttpGet]
        public async Task<IActionResult> MyPage()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);
            var model = new UserViewModel(result);
            model.Friends = await GetAllFriend(model.User);
            return View("User", model);
        }


        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit()
        {
            var user = User;

            var result = _userManager.GetUserAsync(user);

            var editmodel = _mapper.Map<EditViewModel>(result.Result);

            return View("Edit", editmodel);
        }

        [Authorize]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                user.Convert(model);

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage", "User");
                }
                else
                {
                    return RedirectToAction("Edit", "User");
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("Edit", model);
            }
        }

        [Route("UserList")]
        [HttpGet]
        public async Task<IActionResult> UserList(string search)
        {
            var model = CreateSearch(search);
            return View("UserList", model);
        }

        private async Task<SearchViewModel> CreateSearch(string search)
        {
            var currentuser = User;

            var result = await _userManager.GetUserAsync(currentuser);

            var list = _userManager.Users.AsEnumerable().Where(x => x.GetFullName().ToLower().Contains(search.ToLower())).ToList();
            var withfriend = await GetAllFriend();

            var data = new List<UserWithFriendExt>();
            list.ForEach(x =>
            {
                var t = _mapper.Map<UserWithFriendExt>(x);
                t.IsFriendWithCurrent = withfriend.Where(y => y.Id == x.Id || x.Id == result.Id).Count() != 0;
                data.Add(t);
            });

            var model = new SearchViewModel()
            {
                UserList = data
            };

            return model;
        }

        private async Task<List<User>> GetAllFriend(User user)
        {
            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            return repository.GetFriendsByUser(user);
        }

        private async Task<List<User>> GetAllFriend()
        {
            var user = User;

            var result = await _userManager.GetUserAsync(user);

            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            return repository.GetFriendsByUser(result);
        }

        [Route("AddFriend")]
        [HttpPost]
        public async Task<IActionResult> AddFriend(string id)
        {
            var currentUser = User;

            var result = await _userManager.GetUserAsync(currentUser);

            var friend = await _userManager.FindByIdAsync(id);

            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;
            repository.AddFriend(result, friend);

            return RedirectToAction("MyPage", "User");
        }

        [Route("DeleteFriend")]
        [HttpPost]
        public async Task<IActionResult> DeleteFriend(string id)
        {
            var currentUser = User;

            var result = await _userManager.GetUserAsync (currentUser);

            var friend = await _userManager.FindByIdAsync (id);

            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;
            repository.DeleteFriend(result, friend);

            return RedirectToAction("MyPage", "User");
        }
    }
}
