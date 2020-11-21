using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        PagedList<ShapedEntity> GetProducts(ProductParameters productParameters);
        ShapedEntity GetProductById(Guid productId, string fields);
        Product GetProductById(Guid productId);
        void DeleteProduct(Product product);
    }
}
