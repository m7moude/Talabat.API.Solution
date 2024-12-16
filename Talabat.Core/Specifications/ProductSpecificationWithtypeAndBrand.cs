using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecificationWithtypeAndBrand :BaseSpecification<Product>
    {
        public ProductSpecificationWithtypeAndBrand(ProductSpecPram specPram)
            :base(p=> (string.IsNullOrEmpty(specPram.Search) || p.Name.ToLower().Contains(specPram.Search))&&
                      (!specPram.BrandId.HasValue||specPram.BrandId.Value==p.ProductBrandId) &&
                      (!specPram.TypeId.HasValue || specPram.TypeId.Value==p.ProductTypeId))
        {
            Inclides.Add(p=>p.ProductBrand);
            Inclides.Add(p=>p.ProductType);

            if (!string.IsNullOrEmpty(specPram.Sort))
                switch (specPram.Sort)
                {
                    case "price": AddOrderBy(p => p.Price);
                        break;
                    case "name": AddOrderBy(p => p.Name);
                        break;
                    case "priceDesc":AddOrderByDesc(p => p.Price);
                        break;
                    default :
                        break;
                }
            AddPagination(specPram.PageIndex,specPram.PageSize);
        }
        public ProductSpecificationWithtypeAndBrand(int id) : base(p=>p.Id==id) 
        {
            Inclides.Add(p => p.ProductBrand);
            Inclides.Add(p => p.ProductType);
        }

    }
}
