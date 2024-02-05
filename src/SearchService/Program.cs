using MongoDB.Driver;
using MongoDB.Entities;
using SearchService;
using SearchService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseAuthorization();
app.MapControllers();

try
{
    await DbInitializer.InitDb(app);
}
catch (System.Exception e)
{
   Console.WriteLine(e.Message);    
}

app.Run();
