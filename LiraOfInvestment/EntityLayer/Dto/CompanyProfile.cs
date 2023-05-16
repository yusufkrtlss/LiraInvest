using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dto
{
    public class CompanyProfile
    {

        public Profile Profile { get; set; }
        public IEnumerable<SelectListItem> profiles { get; set; }
        public List<News> News { get; set; }
        public FinancialData FinancialData { get; set; }
        public AppUser AppUser { get; set; }
        public List<Profile> TopFiveProfile { get; set; }
        public Prices prices { get; set; }
        public List<IncomeStatement> incomeStatement { get; set; }
        public List<Favorite> favorites { get; set; }
        public List<CorporateEvents> corporateEvents { get; set;}
        public List<KeyExecutives> keyExecutives { get; set;}
    }
}
