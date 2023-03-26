﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfFavoriteDal : GenericRepository<Favorite>, IFavoriteDal
    {
        public List<Favorite> GetFavoritesListWithProfile(int id)
        {
            using var c = new Context();
            return c.Favorites.Where(x => x.AppUserId == id).Include(x => x.Profile).ToList();
        }
    }
}
