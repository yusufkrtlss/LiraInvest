using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserService : IGenericService<AppUser>
    {
        public List<AppUser> GetAppUserIncludeFavoritesList(int id);
    }
}
