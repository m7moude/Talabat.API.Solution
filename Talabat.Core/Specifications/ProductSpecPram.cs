using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductSpecPram
    {
        private const int MaxPage = 10;

        private int pageSize = 5;
        public int PageSize
        {
            set { pageSize = value > MaxPage ? MaxPage : value; }
            get { return pageSize; }
        }

        private string? search ;

        public string? Search
        {
            get { return search; }
            set { search =  string.IsNullOrEmpty(value)?null:value.ToLower(); }
        }


        public int PageIndex { get; set; } = 1;


        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
    }
}
