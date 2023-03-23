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
    public class ProfileManager : IProfileService
    {
        IProfileDal _profileDal;

        public ProfileManager(IProfileDal profileDal)
        {
            _profileDal = profileDal;
        }

        public void TAdd(Profile t)
        {
            _profileDal.Insert(t);
        }

        public void TDelete(Profile t)
        {
            _profileDal.Delete(t);
        }

        public Profile TGetByID(int id)
        {
            return _profileDal.GetByID(id);
        }

        public List<Profile> TGetList()
        {
            return _profileDal.GetList();
        }

        public void TUpdate(Profile t)
        {
            _profileDal.Update(t);
        }
    }

}
