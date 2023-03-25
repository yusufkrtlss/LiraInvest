using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LiraOfInvestment.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProfileService _profileService;

        public AdminController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        [HttpGet("/Admin/Index")]
        public IActionResult Index()
        {
            var values = _profileService.TGetList();
            return View(values);
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
