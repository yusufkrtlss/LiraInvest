using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Linq;

namespace LiraOfInvestment.ViewComponents.Favorites
{
    public class FavoritesList:ViewComponent
    {
        IFavoriteService _favoriteService;
        private readonly UserManager<AppUser> _userManager;
        private IProfileService _profileService;
        public FavoritesList(IFavoriteService favoriteService, UserManager<AppUser> userManager, IProfileService profileService)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
            _profileService = profileService;
        }
        public IViewComponentResult Invoke()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name);
            //var favs = (from p in _profileService.TGetList()
            //            join f in _favoriteService.TGetList()
            //            on p.Id equals f.CompanyId
            //            where user.Id == f.UserId
            //            select p.Symbol).ToList();
            var profiles = _profileService.TGetList();
            var favs = _profileService.TGetList().Join(_favoriteService.TGetList(),compId=>compId.Id,favId=>favId.CompanyId,(compId,favId)=>new {Comp=compId,Fav=favId}).Where(x=>x.Fav.UserId.Equals(user.Id)).ToList();
            
            
            var model = new UserProfile();
            
            return View(model);
        }
    }
}
