using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApi.Application;
using WebApi.Qumulus.Domain;

namespace WebApi.Qumulus.Infrastructure
{
	public class ProductRepository : IProductRepository

	{
        private readonly SalesContext _salesContext;
        public ProductRepository(SalesContext salesContext)
		{
            _salesContext = salesContext;

        }

        public async Task<Product> createProduct(Product product)
        {
           _salesContext.Products.Add(product);
           _salesContext.SaveChanges();

           return product;
        }

        public async Task<bool> deleteProduct(int productId)
        {
            Product products = await _salesContext.Products.Where(p => p.ProductId == productId).SingleOrDefaultAsync();

            if (products != null)
            {
                _salesContext.Products.Remove(products);
                _salesContext.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<List<Product>> getAllProducts(int take, int skip)
        {
            return await _salesContext.Products.OrderBy(p => p.ProductId).Skip(skip).Take(take).ToListAsync();
           
        }

        public async Task<Product> updateProduct(Product product)
        {
            Product products = await _salesContext.Products.Where(p => p.ProductId == product.ProductId).SingleOrDefaultAsync();

            if (products != null)
            {
                products.Name = product.Name;
                products.Description = product.Description;
                products.Price = product.Price;
                products.ProductCategory = product.ProductCategory;
                products.SKU = product.SKU;
            }

            _salesContext.SaveChanges();

            return product;
        }

        //public async Task<Product> createProduct(Product product)
        //{
        //    _salesContext.Products.Add(product);
        //    _salesContext.SaveChanges();

        //    return product;
        //}

        //public bool deleteProduct(int productId)
        //{
        //    Product products = _salesContext.Products.Where(p => p.ProductId == productId).SingleOrDefault();

        //    if (products != null)
        //    {
        //        _salesContext.Products.Remove(products);
        //        _salesContext.SaveChanges();
        //        return true;
        //    }

        //    return false;
        //}

        //public List<Product> getAllProducts(int take, int skip)
        //{
        //    return _salesContext.Products.OrderBy(p => p.ProductId).Skip(skip).Take(take).ToList();
        //}

        //public Product updateProduct(Product product)
        //{
        //    Product products = _salesContext.Products.Where(p => p.ProductId == product.ProductId).SingleOrDefault();


        //    if (products != null)
        //    {
        //        products.Name = product.Name;
        //        products.Description = product.Description;
        //        products.Price = product.Price;
        //        products.ProductCategory = product.ProductCategory;
        //        products.SKU = product.SKU;
        //    }

        //    _salesContext.SaveChanges();

        //    return product;
        //}
    }
}

