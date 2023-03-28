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
    public class FinancialDataManager : IFinancialDataService
    {
        IFinancialDataDal _financialDataDal;
        public FinancialDataManager(IFinancialDataDal financialDataDal)
        {
            _financialDataDal = financialDataDal;
        }

        public void TAdd(FinancialData t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(FinancialData t)
        {
            throw new NotImplementedException();
        }

        public FinancialData TGetByID(int id)
        {
            return _financialDataDal.GetByID(id);
        }

        public List<FinancialData> TGetList()
        {
            return _financialDataDal.GetList();
        }

        public void TUpdate(FinancialData t)
        {
            throw new NotImplementedException();
        }
    }
}
