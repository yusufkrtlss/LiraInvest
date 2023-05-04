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
    public class PricesManager : IPricesService
    {
        private readonly IPricesDal _pricesDal;

        public PricesManager(IPricesDal pricesDal)
        {
            _pricesDal = pricesDal;
        }

        public void TAdd(Prices t)
        {
            _pricesDal.Insert(t);
        }

        public void TDelete(Prices t)
        {
            _pricesDal.Delete(t);
        }

        public Prices TGetByID(int id)
        {
            return _pricesDal.GetByID(id);
        }

        public List<Prices> TGetList()
        {
            return _pricesDal.GetList();
        }

        public void TUpdate(Prices t)
        {
            _pricesDal.Update(t);
        }
    }
}
