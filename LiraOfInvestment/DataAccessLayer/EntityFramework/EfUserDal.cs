using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfUserDal:GenericRepository<AppUser>, IUserDal
    {
        public AppUser GetAppUserWithFavoritesList(int id)
        {
            using var c = new Context();
            var result= c.Users.Where(x => x.Id == id).Include(y => y.Favorites).FirstOrDefault();
            return result;
        }
    }
}
