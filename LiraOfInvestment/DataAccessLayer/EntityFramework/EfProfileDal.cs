using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfProfileDal : GenericRepository<Profile>, IProfileDal
    {
        public List<Profile> GetProfileWithFavoritesList(int id)
        {
            using var c = new Context();
            var result = c.Profiles.Where(x => x.Id == id).Include(y => y.Favorites).ToList();
            return result;
        }
    }
}
