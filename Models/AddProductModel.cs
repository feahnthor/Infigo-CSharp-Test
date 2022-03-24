using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infigo_api_sucks_solution.Models
{
    
    public class AddProductModel
    {
        public string name { get; set; }
        public string designer { get; set; }
        public string[] themes { get; set; }
        public string[] sizes { get; set; }
        public string[] colors { get; set; }
        public string[] tags { get; set; }
        public string action { get; set; }
    }

}

