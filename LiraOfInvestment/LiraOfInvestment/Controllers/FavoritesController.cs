using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LiraOfInvestment.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private IFavoriteService _favoriteService;
        private IProfileService _profileService;
        private readonly UserManager<AppUser> _userManager;

        public FavoritesController(IFavoriteService favoriteService, UserManager<AppUser> userManager, IProfileService profileService)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
            _profileService = profileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFavoriteAsync(int id)
        {
            var company = _profileService.TGetByID(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = new Favorite();
            model.AppUserId = user.Id;
            model.ProfileId = company.Id;

            var allFav = _favoriteService.TGetList().Where(x => x.ProfileId == id).ToList();

            _favoriteService.TAdd(model);
            return Redirect("/Company/Index/" + id);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavoriteAsync(int id)
        {
            var company = _profileService.TGetByID(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var Fav=_favoriteService.TGetList().Where(x=>x.ProfileId == id&&x.AppUserId==user.Id).First();
           
            _favoriteService.TDelete(Fav);


           
            return Redirect("/Company/Index/" + id);
        }


    }
}
