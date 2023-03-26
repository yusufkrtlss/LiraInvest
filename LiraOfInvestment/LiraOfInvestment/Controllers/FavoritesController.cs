using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LiraOfInvestment.Controllers
{
    public class FavoritesController : Controller
    {
        private IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route ("/Favorites/AddFavorite")]
        public IActionResult AddFavorite(Favorite favorite)
        {
            _favoriteService.TAdd(favorite);
            var jsonFavorite = JsonConvert.SerializeObject(favorite);
            return Json(jsonFavorite);
        }
    }
}
