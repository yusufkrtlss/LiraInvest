using BusinessLayer.Abstract;
using EntityLayer.Dto;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiraOfInvestment.ViewComponents.Compare
{
    public class CompareCompany:ViewComponent
    {
        IProfileService _profileService;

        public CompareCompany(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var comp=_profileService.TGetByID(id);
            var companies = _profileService.TGetList();
            ViewData["Companies"] = new SelectList(companies, "Id", "Symbol");

            var list = companies.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Symbol
            });

            var model = new CompanyCompare();
            model.profiles = list;
            
            return View("CompareCompany",model);
        }
    }
}
