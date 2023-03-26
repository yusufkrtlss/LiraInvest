using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dto
{
    public class AddFavoriteModel
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
    }
}
