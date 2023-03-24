using BusinessLayer.Abstract.Charts;
using DataAccessLayer.Abstract.Charts;
using EntityLayer.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Charts
{
    public class BarChartYearlyManager : IBarChartYearlyService
    {
        private IBarChartDal _barChartDal;

        public BarChartYearlyManager(IBarChartDal barChartDal)
        {
            _barChartDal = barChartDal;
        }

        public void TAdd(BarChartYearly t)
        {
            _barChartDal.Insert(t);
        }

        public void TDelete(BarChartYearly t)
        {
            _barChartDal.Delete(t);
        }

        public BarChartYearly TGetByID(int id)
        {
            return _barChartDal.GetByID(id);
        }

        public List<BarChartYearly> TGetList()
        {
            return _barChartDal.GetList();
        }

        public void TUpdate(BarChartYearly t)
        {
            _barChartDal.Update(t);
        }
    }
}
