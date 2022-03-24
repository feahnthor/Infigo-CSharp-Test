using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infigo_api_sucks_solution.Models
{
    public class CategoryDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] ProductIds { get; set; }
        public int[] SubCategoryIds { get; set; }
        public MisconfigurationModel[] MisConfigurations { get; set; }
    }
}
