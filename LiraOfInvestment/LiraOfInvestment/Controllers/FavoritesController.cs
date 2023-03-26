using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LiraOfInvestment.Controllers
{
    [AllowAnonymous]
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
            //var company=_companyService.TGetByID(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = new Favorite2();
            model.UserId = user.Id;
            model.CompanyId = id;
            var allFav = _favoriteService.TGetList().Where(x=>x.CompanyId==id).ToList();
            foreach(var fav in allFav)
            {
                if(fav.UserId==user.Id)
                {
                    return Redirect("/Company/Index/" + id);
                }
                else
                {
                    _favoriteService.TAdd(model);
                }
            }
           
            

            return Redirect("/Company/Index/"+id);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name);
            var favs = (from f in _favoriteService.TGetList()
                       join p in _profileService.TGetList()
                       on f.CompanyId equals p.Id
                       where f.UserId == user.Id
                       select p.Symbol).ToList();
            var model = new Favorite2();
            return View();
        }
    }
}
