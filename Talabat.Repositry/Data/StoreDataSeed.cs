using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggrate;

namespace Talabat.Repositry.Data
{
    public static class StoreDataSeed
    {
        public static async Task SeedAsync(StoreDbcontext dbcontext)
        {
            if (!dbcontext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Talabat.Repositry/Data/DataSeed/brands.json");
                var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brand?.Count > 0)
                {
                    foreach (var item in brand)
                        await dbcontext.Set<ProductBrand>().AddAsync(item);

                    await dbcontext.SaveChangesAsync();
                }
            }
            if (!dbcontext.ProductTypes.Any())
            {
                var typeData = File.ReadAllText("../Talabat.Repositry/Data/DataSeed/types.json");
                var type = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                if (type?.Count > 0)
                {
                    foreach (var item in type)
                        await dbcontext.Set<ProductType>().AddAsync(item);

                    await dbcontext.SaveChangesAsync();
                }
            }
            if (!dbcontext.Products.Any())
            {
                var productData = File.ReadAllText("../Talabat.Repositry/Data/DataSeed/products.json");
                var product = JsonSerializer.Deserialize<List<Product>>(productData);
                if (product?.Count > 0)
                {
                    foreach (var item in product)
                        await dbcontext.Set<Product>().AddAsync(item);

                    await dbcontext.SaveChangesAsync();
                }
            }
            if (!dbcontext.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText("../Talabat.Repositry/Data/DataSeed/delivery.json");
                var deliveryMethode = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                if (deliveryMethode?.Count > 0)
                {
                    foreach (var item in deliveryMethode)
                        await dbcontext.Set<DeliveryMethod>().AddAsync(item);

                    await dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}
