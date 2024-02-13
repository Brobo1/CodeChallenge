using Xunit;
using CodeChallenge.Data;
using CodeChallenge.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using CodeChallenge.Models;

public class ProductControllerTests
{
    private readonly DbContextOptions<CodeChallengeDbContext> _options;

    public ProductControllerTests()
    {
        _options = new DbContextOptionsBuilder<CodeChallengeDbContext>()
            .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetProducts_ReturnsProducts()
    {
        using (var context = new CodeChallengeDbContext(_options))
        {
            var product1 = new Product { Name = "Product 1", Price = 100 };
            var product2 = new Product { Name = "Product 2", Price = 200 };

            context.Products.Add(product1);
            context.Products.Add(product2);

            await context.SaveChangesAsync();

            var controller = new ProductController(context);

            var result = await controller.GetProducts() as OkObjectResult;

            Assert.NotNull(result);
            var products = result.Value as List<Product>;

            Assert.Equal(2, products.Count);
            Assert.Contains(product1, products);
            Assert.Contains(product2, products);
        }
    }

    [Fact]
    public async Task CreateProduct_IncreasesCount()
    {
        using (var context = new CodeChallengeDbContext(_options))
        {
            var controller = new ProductController(context);
            
            var productToAdd = new ProductObject { Name = "Product 3", Price = 300 };
            
            await controller.CreateProduct(productToAdd);
            
            var result = await controller.GetProducts() as OkObjectResult;

            Assert.NotNull(result);
            var products = result.Value as List<Product>;

            Assert.Single(products);
        }
    }

    [Fact]
    public async Task DeleteProduct_DecreasesCount()
    {
        using (var context = new CodeChallengeDbContext(_options))
        {
            var product = new Product { Name = "Product 4", Price = 400 };
            
            context.Products.Add(product);

            await context.SaveChangesAsync();

            var controller = new ProductController(context);

            await controller.DeleteProduct(product.Id);

            var result = await controller.GetProducts() as OkObjectResult;

            Assert.NotNull(result);
            var products = result.Value as List<Product>;

            Assert.Empty(products);
        }
    }

    [Fact]
    public async Task UpdateProduct_ChangesProperties()
    {
        using (var context = new CodeChallengeDbContext(_options))
        {
            var product = new Product { Name = "Product 5", Price = 500 };
            
            context.Products.Add(product);

            await context.SaveChangesAsync();

            var controller = new ProductController(context);

            var productToUpdate = new ProductObject { Name = "Updated Product", Price = 600 };

            await controller.UpdateProduct(product.Id, productToUpdate);

            var result = await controller.GetProduct(product.Id) as OkObjectResult;

            Assert.NotNull(result);
            var updatedProduct = result.Value as Product;

            Assert.Equal(productToUpdate.Name, updatedProduct.Name);
            Assert.Equal(productToUpdate.Price, updatedProduct.Price);
        }
    }

    [Fact]
    public async Task GetProduct_ReturnsCorrectProduct()
    {
        using (var context = new CodeChallengeDbContext(_options))
        {
            var product = new Product { Name = "Product 6", Price = 600 };
            
            context.Products.Add(product);

            await context.SaveChangesAsync();

            var controller = new ProductController(context);

            var result = await controller.GetProduct(product.Id) as OkObjectResult;

            Assert.NotNull(result);
            var retrievedProduct = result.Value as Product;

            Assert.Equal(product.Name, retrievedProduct.Name);
            Assert.Equal(product.Price, retrievedProduct.Price);
        }
    }
}