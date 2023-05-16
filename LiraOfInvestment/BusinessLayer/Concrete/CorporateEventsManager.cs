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
    public class CorporateEventsManager : ICorporateEventsService
    {
        private ICorporateEventsDal _corporateEventsDal;

        public CorporateEventsManager(ICorporateEventsDal corporateEventsDal)
        {
            _corporateEventsDal = corporateEventsDal;
        }

        public void TAdd(CorporateEvents t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(CorporateEvents t)
        {
            throw new NotImplementedException();
        }

        public CorporateEvents TGetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<CorporateEvents> TGetList()
        {
            return _corporateEventsDal.GetList();
        }

        public void TUpdate(CorporateEvents t)
        {
            throw new NotImplementedException();
        }
    }
}
