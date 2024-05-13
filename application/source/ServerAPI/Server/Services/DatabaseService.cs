#nullable enable

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerAPI.Server.Core.Encryption;
using ServerAPI.Server.Database;
using ServerAPI.Server.Models.Users;
using SharedLibrary.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ServerAPI.Server.Services
{
    public class DatabaseService
    {
        //private AppSettingsService AppSettings { get; set; }
        private DatabaseContext DbContext { get; set; }
        //private readonly IServiceScopeFactory ScopeFactory;

        public DatabaseService(DatabaseContext database/*IServiceScopeFactory scopeFactory*/)
        {
            //AppSettings = appSettings;
            //ScopeFactory = scopeFactory;
            DbContext = database;
        }

        //public DatabaseContext Connect()
        //{
        //    // Create database options to connect to the database
        //    var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        //    optionsBuilder.UseMySQL(AppSettings.ConnectionString);

        //    // Connect to the database
        //    return new DatabaseContext(optionsBuilder.Options, AppSettings.Configuration);
        //}

        public void Migrate()
        {
            //try
            //{
            //    //DatabaseContext db = Connect();
            //    //context.Database.EnsureCreated();
            //    //db.Database.Migrate();
            //}
            //catch (Exception ex)
            //{
            //    // TODO
            //}
        }

        public async Task<bool> CheckDatabaseConnectionAsync()
        {
            // Problema: https://github.com/aspnet/EntityFrameworkCore/issues/13235
            // Fix com CanConnect(): https://github.com/aspnet/EntityFrameworkCore/pull/13289
            try
            {
                await DbContext.Database.CanConnectAsync();
            }
            catch (Exception ex)
            {
                // TODO: Add to log file.
                return false;
            }
            return true;
        }

        public async Task<UserModel> AddOrRetrieveUserAsync(UserModel user)
        {
            UserModel? userFromDatabase = await this.RetrieveUseAsync(user);
            if (userFromDatabase == null)
            {
                await DbContext.Users.AddAsync(user);
                await DbContext.SaveChangesAsync();
                return user;
            }
            return userFromDatabase;
        }

        public async Task<UserModel?> RetrieveUseAsync(UserModel user)
        {
            return await Task.Run(() => DbContext.Users.Where(u => u.GoogleId == user.GoogleId).FirstOrDefault());
        }

        public UserModel? RetrieveUser(long id, string password)
        {
            UserModel user = DbContext.Users.Find(id);

            //db.Dispose();

            if (user != null && user.Password == password)
            {
                return user;
            }

            return null;
        }

        public async Task<string?> AddUserTokenLoginAsync()
        {
            UserTokenLoginModel userTokenLogin = new UserTokenLoginModel();
            Encryption encryption = new Encryption();

            string token = Guid.NewGuid().ToString();
            HashWithSaltResult result = encryption.HashWithSalt(token, HashAlgorithm.Create("SHA-256"));

            userTokenLogin.ExpirationDate = DateTime.Now.AddDays(1);
            userTokenLogin.Token = result.Digest;

            await DbContext.Users_Tokens_Login.AddAsync(userTokenLogin);

            await DbContext.SaveChangesAsync();

            //await db.DisposeAsync();

            return result.Digest;
        }

        public async Task<UserTokenLoginModel?> GetLoginTokenAsync(string loginToken)
        {
            //UserTokenLoginModel user = await DbContext.Users_Tokens_Login.Where(t => t.Token == loginToken).FirstOrDefaultAsync();
            //Console.WriteLine("--------------");
            //Console.WriteLine(loginToken);
            //Console.WriteLine(user.Id);
            //Console.WriteLine(user.Token);
            //Console.WriteLine(user.ExpirationDate);
            //Console.WriteLine("--------------");
            return await DbContext.Users_Tokens_Login.Where(t => t.Token == loginToken).FirstOrDefaultAsync();
        }

        public async Task DeleteLoginToken(UserTokenLoginModel loginToken)
        {
            DbContext.Users_Tokens_Login.Remove(loginToken);
            await DbContext.SaveChangesAsync();
        }
    }
}
