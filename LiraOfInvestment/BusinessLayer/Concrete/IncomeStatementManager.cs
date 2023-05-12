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
    public class IncomeStatementManager : IIncomeStatementService
    {
        IIncomeStatementDal _incomeStatementDal;

        public IncomeStatementManager(IIncomeStatementDal incomeStatementDal)
        {
            _incomeStatementDal = incomeStatementDal;
        }

        public void TAdd(IncomeStatement t)
        {
            _incomeStatementDal.Insert(t);
        }

        public void TDelete(IncomeStatement t)
        {
            _incomeStatementDal.Delete(t);
        }

        public IncomeStatement TGetByID(int id)
        {
            return _incomeStatementDal.GetByID(id);
        }

        public List<IncomeStatement> TGetList()
        {
            return _incomeStatementDal.GetList();
        }

        public void TUpdate(IncomeStatement t)
        {
            _incomeStatementDal.Update(t);
        }
    }
}
