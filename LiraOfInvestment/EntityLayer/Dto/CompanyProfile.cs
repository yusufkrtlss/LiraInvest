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
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        
        public IEnumerable<SelectListItem> profiles { get; set; }
        public List<News> News { get; set; }
        public FinancialData FinancialData { get; set; }
        public AppUser AppUser { get; set; }
        public List<Profile> TopFiveProfile { get; set; }
        public Prices prices { get; set; }
    }
}
