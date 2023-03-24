using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LiraOfInvestment.ViewComponents.BarChart
{
    public class BarChart:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //var values = teamManager.TGetList();
            return View();
        }
    }
}
