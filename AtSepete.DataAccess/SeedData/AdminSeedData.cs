using AtSepete.DataAccess.Context;
using AtSepete.Entities.Data;
using AtSepete.Entities.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AtSepete.DataAccess.SeedData
{
    public static class AdminSeedData
    {
        private const string AdminEmail = "admin@atsepete.com";
        private const string AdminPassword = "Admin123";
        public static async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<AtSepeteDbContext>();

            dbContextBuilder.UseSqlServer(configuration.GetConnectionString(AtSepeteDbContext.ConnectionName));

            using AtSepeteDbContext context = new(dbContextBuilder.Options);

            if (!context.Users.Any(user => user.Email == AdminEmail))
            {
                await AddAdmin(context);
            }
            await Task.CompletedTask;

        }
        private static async Task AddAdmin(AtSepeteDbContext context)
        {
            User user = new User()
            {
                FirstName = "Admin",
                LastName = "SuperAdmin",
                Email = AdminEmail,
                Password = AdminPassword,
                AccessFailedCount = 0,
                Adress = "Istanbul",
                BirthDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                Gender = Gender.Male,
                PhoneNumber = "05555555555",
                LockoutEnabled = true,
                IsActive = true,
                Role = Role.Admin

            };
            user.Password = await PasswordHashAsync(AdminPassword);
            await context.AddAsync(user);
            await context.SaveChangesAsync();
        }
        private static async Task<string> PasswordHashAsync(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);//salt, her kullanıcı için farklıdır ve hash'in sonucunu değiştirir. Bu nedenle, aynı şifreyi kullanan iki kullanıcının hash'leri farklı olacaktır.
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

    }
}
