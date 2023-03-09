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
    public class ShareManager : IShareService
    {
        IShareDal _shareDal;

        public ShareManager(IShareDal shareDal)
        {
            _shareDal = shareDal;
        }

        public void TAdd(Share t)
        {
            _shareDal.Insert(t);
        }

        public void TDelete(Share t)
        {
            _shareDal.Delete(t);
        }

        public Share TGetByID(int id)
        {
            return _shareDal.GetByID(id);
        }

        public List<Share> TGetList()
        {
            return _shareDal.GetList();
        }

        public void TUpdate(Share t)
        {
            _shareDal.Update(t);
        }
    }
}
