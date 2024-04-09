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
    public static class MembersSeeder
    {
        private static List<Member> _members;
        private static EMartDbContext _dbContext;
        static MembersSeeder() { _members = new List<Member>(); }
        public static async Task Seed(EMartDbContext dbContext)
        {
            _dbContext = dbContext;
            if (await _dbContext.Members.AnyAsync())
                await ResetMembersTable();
            await GetAllMembersFromJson();
            await InsertMembersToDatabase();
        }
        private static async Task ResetMembersTable()
        {
            await _dbContext.Database.ExecuteSqlRawAsync($"DBCC CHECKIDENT('Members', RESEED, 0)");
        }
        private static Task GetAllMembersFromJson()
        {
            return Task.Run(() =>
            {
                _members.Clear();
                _members = JsonUtils.DeserializeJsonList<Member>(FileDirectories.MemberJsonPath);
            });
        }
        private static async Task InsertMembersToDatabase()
        {
            foreach (var member in _members)
            {
                await _dbContext.Members.AddAsync(member);
            }
            await _dbContext.SaveChangesAsync();

        }
    }
}

