using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Services;

namespace SearchService.Data
{
    public class DbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("SearchDb", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Item>()
                .Key(a => a.Make, KeyType.Text)
                .Key(a => a.Model, KeyType.Text)
                .Key(a => a.Color, KeyType.Text)
                .CreateAsync();

            var count = await DB.CountAsync<Item>();
            using var scope = app.Services.CreateScope();
            var auctionServiceHttpClient = scope.ServiceProvider.GetRequiredService<AuctionServiceHttpClient>();

            var items = await auctionServiceHttpClient.GetItemsForSearchDb();
            Console.WriteLine(items.Count + " items fetched from AuctionService");
           if(items.Count > 0) await DB.InsertAsync(items);
        }
    }
}