using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Charts;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Charts;
using EntityLayer.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Charts
{
    public class TwoYearsMonthlyManager : ITwoYearsMonthly
    {
        ITwoYearsMonthlyDal _twoyearsmonthlyDal;

        public TwoYearsMonthlyManager(ITwoYearsMonthlyDal twoyearsmonthlyDal)
        {
            _twoyearsmonthlyDal = twoyearsmonthlyDal;
        }
        public void TAdd(Two_Years_Monthly_Chart t)
        {
            _twoyearsmonthlyDal.Insert(t);
        }

        public void TDelete(Two_Years_Monthly_Chart t)
        {
            _twoyearsmonthlyDal.Delete(t);
        }

        public Two_Years_Monthly_Chart TGetByID(int id)
        {
            return _twoyearsmonthlyDal.GetByID(id);
        }

        public List<Two_Years_Monthly_Chart> TGetList()
        {
            return _twoyearsmonthlyDal.GetList();
        }

        public void TUpdate(Two_Years_Monthly_Chart t)
        {
            _twoyearsmonthlyDal.Update(t);
        }
    }
}
