using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace LiraOfInvestment.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IFavoriteService _favoriteService;
        private readonly IUserService _userService;
        private readonly INewsService _newsService;
        private readonly UserManager<AppUser> _userManager;
        public AdminController(IProfileService profileService, IFavoriteService favoriteService, IUserService userService, UserManager<AppUser> userManager, INewsService newsService)
        {
            _profileService = profileService;
            _favoriteService = favoriteService;
            _userService = userService;
            _userManager = userManager;
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _profileService.TGetList().ToList();
            var favorites = _favoriteService.TGetList();
            var userfavs = _userService.GetAppUserIncludeFavoritesList(user.Id);
            var news = _newsService.TGetList().Take(5).ToList();
            //var favs=userfavs.Favorites.Any(t => t.AppUserId);

            var f2=_favoriteService.GetFavoritesListIncludeProfile(user.Id);
            var model = new UserProfile();
            model.profiles = values;
            model.favs = f2;
            model.news = news;
            return View(model);
        }
        public PartialViewResult PartialNavbar()
        {
            return PartialView();
        }
        public PartialViewResult PartialHead()
        {
            return PartialView();
        }
        public PartialViewResult PartialScript()
        {
            return PartialView();
        }
        public PartialViewResult PartialFooter()
        {
            return PartialView();
        }
    }
}
