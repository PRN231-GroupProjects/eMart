using eMart_Repository.Constants;
using eMart_Repository.Entities;
using eMart_Repository.Migrations;
using eMart_Repository.Seed.ChildSeeders;
using eMart_Repository.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMart_Repository.Seed
{
    public static class DataSeeder
    {
        private static EMartDbContext _dbContext;
        public static async Task Seed(EMartDbContext context)
        {
            _dbContext = context;
            await CategoriesSeeder.Seed(_dbContext);
            await ProductsSeeder.Seed(_dbContext);
            await MembersSeeder.Seed(_dbContext);
        }

    }
}
