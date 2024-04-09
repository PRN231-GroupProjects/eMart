using eMart_Repository.Constants;
using eMart_Repository.Entities;
using eMart_Repository.Migrations;
using eMart_Repository.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMart_Repository.Seed.ChildSeeders
{
    public static class CategoriesSeeder
    {
        private static EMartDbContext _dbContext;
        private static List<Category> _categories;
        static CategoriesSeeder() { _categories = new List<Category>(); }
        public static async Task Seed(EMartDbContext dbContext)
        {
            _dbContext = dbContext;
            if (await _dbContext.Categories.AnyAsync())
                await ResetCategoriesTable();
            await GetAllCategoriesFromJson();
            await InsertCategoriesToDatabase();
        }
        private static async Task ResetCategoriesTable()
        {            
            await _dbContext.Database.ExecuteSqlRawAsync($"DBCC CHECKIDENT('Categories', RESEED, 0)");
        }
        private static Task GetAllCategoriesFromJson()
        {
            return Task.Run(() =>
            {
                _categories.Clear();
                _categories = JsonUtils.DeserializeJsonList<Category>(FileDirectories.CategoryJsonPath);
            });
        }
        private static async Task InsertCategoriesToDatabase()
        {
            foreach (var category in _categories)
            {
                await _dbContext.Categories.AddAsync(category);
            }
            await _dbContext.SaveChangesAsync();

        }
    }
}
