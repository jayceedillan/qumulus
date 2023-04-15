using System;
using WebApi.Application;
using WebApi.Qumulus.Domain;

namespace WebApi.Qumulus.Application
{
	public class ProductService : IProductService
	{
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
           
		{
            _productRepository = productRepository;
        }

        public async Task<Product> createProduct(Product product)
        {
           await _productRepository.createProduct(product);
            return product;
        }

        public async Task<bool> deleteProduct(int productId)
        {
          return await _productRepository.deleteProduct(productId);
        }

        public async Task<List<Product>> getAllProducts(int take, int skip)
        {
            var products = await _productRepository.getAllProducts(take, skip);
            return products;
        }

        public async Task<Product> updateProduct(Product product)
        {
          await  _productRepository.updateProduct(product);
            return product;
        }
    }
}

