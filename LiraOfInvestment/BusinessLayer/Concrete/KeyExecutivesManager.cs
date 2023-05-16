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
    public class KeyExecutivesManager : IKeyExecutivesService
    {
        IKeyExecutivesDal _keyExecutivesDal;

        public KeyExecutivesManager(IKeyExecutivesDal keyExecutivesDal)
        {
            _keyExecutivesDal = keyExecutivesDal;
        }

        public void TAdd(KeyExecutives t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(KeyExecutives t)
        {
            throw new NotImplementedException();
        }

        public KeyExecutives TGetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<KeyExecutives> TGetList()
        {
            return _keyExecutivesDal.GetList();
        }

        public void TUpdate(KeyExecutives t)
        {
            throw new NotImplementedException();
        }
    }
}
