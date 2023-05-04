using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EntityLayer.Dto
{
    public class CompanyCompare
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        //public int CompareId { get; set; }
        public int SelectedId { get; set; }
        public IEnumerable<SelectListItem> profiles { get; set; }

    }
}
