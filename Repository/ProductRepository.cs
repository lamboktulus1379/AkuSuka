using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly ISortHelper<Product> _sortHelper;
        private readonly IDataShaper<Product> _dataShaper;

        public ProductRepository(RepositoryContext repositoryContext,
            ISortHelper<Product> sortHelper,
            IDataShaper<Product> dataShaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }
        public PagedList<ShapedEntity> GetProducts(ProductParameters productParameters)
        {
            var products = FindAll();

            var sortedProducts = _sortHelper.ApplySort(products, productParameters.OrderBy);
            var shapedProducts = _dataShaper.ShapeData(sortedProducts, productParameters.Fields);

            return PagedList<ShapedEntity>.ToPagedList(shapedProducts, productParameters.PageNumber, productParameters.PageSize);
        }

        public ShapedEntity GetProductById(Guid productId, string fields)
        {
            var product = FindByCondition(product => product.Id.Equals(productId)).FirstOrDefault();

            return _dataShaper.ShapeData(product, fields);
        }

        public Product GetProductById(Guid productId)
        {
            return FindByCondition(product => product.Id.Equals(productId))
                //.DefaultIfEmpty(new Product())
                .FirstOrDefault();
        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }
    }
}
