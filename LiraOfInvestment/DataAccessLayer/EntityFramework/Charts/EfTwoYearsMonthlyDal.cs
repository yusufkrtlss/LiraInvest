using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Charts;
using DataAccessLayer.Repository;
using EntityLayer.Charts;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework.Charts
{
    public class EfTwoYearsMonthlyDal : GenericRepository<Two_Years_Monthly_Chart>, ITwoYearsMonthlyDal
    {
    }
}
