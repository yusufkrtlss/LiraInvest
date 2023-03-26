using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace LiraOfInvestment.ViewComponents.Favorites
{
    public class FavoritesList:ViewComponent
    {
        IFavoriteService _favoriteService;
        private readonly UserManager<AppUser> _userManager;
        private IProfileService _profileService;
        private IUserService _manager;
        public FavoritesList(IFavoriteService favoriteService, UserManager<AppUser> userManager, IProfileService profileService, IUserService manager)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
            _profileService = profileService;
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //var favs = (from p in _profileService.TGetList()
            //            join f in _favoriteService.TGetList()
            //            on p.Id equals f.CompanyId
            //            where user.Id == f.UserId
            //            select p.Symbol).ToList();
            // var favs = _favoriteService.TGetList().AsQueryable().Include(x=>x.Profile).Where(x=>x.AppUserId==user.Id).ToList();
            //var fav2=favs.Include(x=>x.Profile).Include(y=>y.Profile.Select(z=>z.Symbol)).Where(t=>t.UserId== user.Id).ToList();
            var fav = _manager.GetAppUserIncludeFavoritesList(user.Id);
            var favs=_favoriteService.GetFavoritesListIncludeProfile(user.Id).ToList();

            var model = new UserProfile();
            model.favs = favs;
            return View(model);
        }
    }
}
