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
    public class NewsManager : INewsService
    {
        INewsDal _newsDal;

        public NewsManager(INewsDal newsDal)
        {
            _newsDal = newsDal;
        }

        public void TAdd(News t)
        {
            _newsDal.Insert(t);
        }

        public void TDelete(News t)
        {
            _newsDal.Delete(t);
        }

        public News TGetByID(int id)
        {
            return _newsDal.GetByID(id);
        }

        public List<News> TGetList()
        {
            return _newsDal.GetList();
        }

        public void TUpdate(News t)
        {
            _newsDal.Update(t);
        }
    }
}
