using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace LiraOfInvestment.ViewComponents.Team
{
    public class TeamList : ViewComponent
    {
        TeamManager teamManager = new TeamManager(new EfTeamDal()); 

        public IViewComponentResult Invoke()
        {
            var values = teamManager.TGetList();
            return View(values);
        }
    }
}
