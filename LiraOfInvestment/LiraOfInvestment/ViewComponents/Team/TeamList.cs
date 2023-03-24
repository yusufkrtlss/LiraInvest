using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace LiraOfInvestment.ViewComponents.Team
{
    public class TeamList : ViewComponent
    {
        // TeamManager teamManager = new TeamManager(new EfTeamDal()); 
        ITeamService _teamService;

        public TeamList(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _teamService.TGetList();
            return View(values);
        }
    }
}
