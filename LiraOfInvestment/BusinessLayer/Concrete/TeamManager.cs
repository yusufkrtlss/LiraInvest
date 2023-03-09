using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TeamManager : ITeamService
    {
        ITeamDal _teamDal;

        public TeamManager(ITeamDal teamDal)
        {
            _teamDal = teamDal;
        }

        public void TAdd(Team t)
        {
            _teamDal.Insert(t);
        }

        public void TDelete(Team t)
        {
            _teamDal.Delete(t);
        }

        public Team TGetByID(int id)
        {
            return _teamDal.GetByID(id);   
        }

        public List<Team> TGetList()
        {
            return _teamDal.GetList();
        }

        public void TUpdate(Team t)
        {
            _teamDal.Update(t);
        }
    }
}
