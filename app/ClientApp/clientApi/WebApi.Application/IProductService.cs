using System;
using WebApi.Qumulus.Domain;

namespace WebApi.Qumulus.Application
{
	public interface IProductService
	{
        Task<List<Product>> getAllProducts(int take, int skip);
        Task<Product> createProduct(Product product);
        Task<Product> updateProduct(Product product);
        Task<bool> deleteProduct(int productId);
    }
}

