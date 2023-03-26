using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dto
{
    public class UserProfile
    {
        public List<Profile> profiles { get; set; }
        public List<news2> news { get; set; }
        public List<string> favs { get; set; }
    }
}
