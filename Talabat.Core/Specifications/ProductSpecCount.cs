using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecCount:BaseSpecification<Product>
    {
        public ProductSpecCount(ProductSpecPram specPram)
            : base(p => (string.IsNullOrEmpty(specPram.Search) || p.Name.ToLower().Contains(specPram.Search)) &&
                      (!specPram.BrandId.HasValue || specPram.BrandId.Value == p.ProductBrandId) &&
                      (!specPram.TypeId.HasValue || specPram.TypeId.Value == p.ProductTypeId))
        {

        }
    }
}
