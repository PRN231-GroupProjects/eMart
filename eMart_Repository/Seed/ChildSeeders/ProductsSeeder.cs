using eMart_Repository.Constants;
using eMart_Repository.Entities;
using eMart_Repository.Migrations;
using eMart_Repository.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMart_Repository.Seed.ChildSeeders
{
    public static class ProductsSeeder
    {
        private static List<Product> _products;
        private static EMartDbContext _dbContext;
        static ProductsSeeder() { _products = new List<Product>(); }
        public static async Task Seed(EMartDbContext dbContext)
        {
            _dbContext = dbContext;
            if (await _dbContext.Products.AnyAsync())
                await ResetProductsTable();
            await GetAllProductsFromJson();
            await InsertProductsToDatabase();
        }
        private static async Task ResetProductsTable()
        {
            await _dbContext.Database.ExecuteSqlRawAsync($"DBCC CHECKIDENT('Products', RESEED, 0)");
        }
        private static Task GetAllProductsFromJson()
        {
            return Task.Run(() =>
            {
                _products.Clear();
                _products = JsonUtils.DeserializeJsonList<Product>(FileDirectories.ProductJsonPath);
            });
        }
        private static async Task InsertProductsToDatabase()
        {
            foreach (var product in _products)
            {
                await _dbContext.Products.AddAsync(product);
            }
            await _dbContext.SaveChangesAsync();

        }
    }
}
