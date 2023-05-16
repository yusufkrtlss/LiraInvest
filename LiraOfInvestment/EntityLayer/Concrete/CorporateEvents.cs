using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CorporateEvents
    {
        public int Id { get; set; }
        public DateTime Date {  get; set; }
        public string Significance { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string ParentTopics { get; set; }
        public string Symbol { get; set; }
    }
}
