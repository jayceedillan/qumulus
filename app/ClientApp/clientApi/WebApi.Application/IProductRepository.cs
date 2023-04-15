using System;
using WebApi.Qumulus.Domain;
namespace WebApi.Application
{
	public interface IProductRepository
	{
        Task<List<Product>> getAllProducts(int take, int skip);
        Task<Product> createProduct(Product product);
        Task<Product> updateProduct(Product product);
        Task<bool> deleteProduct(int productId);
    }
}

