using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class ProductExtensions
    {
        public static void Map(this Product dbProduct, Product product)
        {
            dbProduct.SKU = product.SKU;
            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.Length = product.Length;
            dbProduct.Width = product.Width;
            dbProduct.Height = product.Height;
            dbProduct.Diameter = product.Diameter;
            dbProduct.Description = product.Description;
            dbProduct.Seen = product.Seen;
            dbProduct.Stock = product.Stock;
            dbProduct.Weight = product.Weight;
        }
    }
}
