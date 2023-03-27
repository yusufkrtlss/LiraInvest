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
            model.AppUserId=user.Id;
            model.ProfileId = company.Id;
            //var favs = _favoriteService.TGetList(company, user);
            //_favoriteService.TAdd(model);
            //model.UserId = user.Id;
            //model.ProfileId = id;
            var allFav = _favoriteService.TGetList().Where(x => x.ProfileId == id).ToList();
            //foreach (var fav in allFav)
            //{
            //    if (fav.AppUserId == user.Id)
            //    {
            //        return Redirect("/Company/Index/" + id);
            //    }
            //    else
            //    {
            //        
            //    }
            //}


            _favoriteService.TAdd(model);
            return Redirect("/Admin/Index/"+id);
        }

        
    }
}
