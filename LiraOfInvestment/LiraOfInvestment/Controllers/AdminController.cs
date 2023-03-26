using BusinessLayer.Abstract;
using EntityLayer.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiraOfInvestment.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {
        private readonly IProfileService _profileService;

        public AdminController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        
        public IActionResult Index()
        {
            var values = _profileService.TGetList().Take(9).ToList();
            var model = new UserProfile();
            model.profiles = values;
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
