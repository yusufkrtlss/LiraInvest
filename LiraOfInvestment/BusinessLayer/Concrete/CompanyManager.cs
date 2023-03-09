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
    public class CompanyManager : ICompanyService
    {
        ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public void TAdd(Company t)
        {
            _companyDal.Insert(t);
        }

        public void TDelete(Company t)
        {
            _companyDal.Delete(t);
        }

        public Company TGetByID(int id)
        {
            return _companyDal.GetByID(id);
        }

        public List<Company> TGetList()
        {
            return _companyDal.GetList();
        }

        public void TUpdate(Company t)
        {
            _companyDal.Update(t);
        }
    }
}
