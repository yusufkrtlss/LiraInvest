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
    public class FavoriteManager : IFavoriteService
    {
        IFavoriteDal _favoriteDal;

        public FavoriteManager(IFavoriteDal favoriteDal)
        {
            _favoriteDal = favoriteDal;
        }

        public void TAdd(Favorite t)
        {
            _favoriteDal.Insert(t);
        }

        public void TDelete(Favorite t)
        {
            _favoriteDal.Delete(t);
        }

        public Favorite TGetByID(int id)
        {
            return _favoriteDal.GetByID(id);
        }

        public List<Favorite> TGetList()
        {
            return _favoriteDal.GetList();
        }

        public void TUpdate(Favorite t)
        {
            _favoriteDal.Update(t);
        }
    }
}
