using EntityLayer.Charts;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:liraofinvestment.database.windows.net,1433;Initial Catalog=LiraDb;Persist Security Info=False;User ID=lirainvestment;Password=Lira2022;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        public DbSet<Company> Companies { get; set; }      
        public DbSet<Favorite2> Favorites4 { get; set; }
        public DbSet<news2> news2 { get; set; } 
        public DbSet<Share> Shares { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Profile> profile_fast_info2 { get; set; }
        public DbSet<Two_Years_Monthly_Chart> two_years_monthly { get; set; }
        public DbSet<BarChartYearly> annual_revenue { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Favorite2>()
                .HasKey(m => new { m.UserId, m.CompanyId });
        }

    }
}
