using BusinessLayer.Abstract.Charts;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiraOfInvestment.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        IProfileService _profileService;
        IFinancialDataService _financialDataService;

        public DefaultController(IProfileService profileService, IFinancialDataService financialDataService)
        {
            _profileService = profileService;
            _financialDataService = financialDataService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult HeaderPartial()
        {
            return PartialView();
        }

        public PartialViewResult NavbarPartial()
        {
            return PartialView();
        }

        public PartialViewResult CarouselPartial()
        {
            return PartialView();
        }

        public PartialViewResult FeaturePartial()
        {
            return PartialView();
        }

        public PartialViewResult PricingPartial()
        {
            return PartialView();
        }

        public PartialViewResult ContactPartial()
        {
            return PartialView();
        }

        public PartialViewResult ScriptPartial()
        {
            return PartialView();
        }
        [Authorize]
        [Produces("application/json")]
        [ValidateAntiForgeryToken]
        [Route("/default/search")]
        public JsonResult SearchAsync()
        {
            string term = HttpContext.Request.Query["term"].ToString().ToUpper();
            var query = _financialDataService.TGetList().Where(i=>i.Symbol.Contains(term)).Select(x=> new {id=x.Id,symbol=x.Symbol});

            return Json(query);
        }

        

    }
}
