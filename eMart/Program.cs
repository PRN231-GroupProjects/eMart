using System.Reflection;
using System.Text.Json.Serialization;
using eMart.Config;
using eMart_Repository.Migrations;
using eMart_Repository.Seed;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddUnitOfWork();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddDatabase();
builder.Services.AddServices();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<EMartDbContext>();
await context.Database.EnsureDeletedAsync();
await context.Database.MigrateAsync();
await DataSeeder.Seed(context); 
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSession();
app.MapControllers();

app.Run();
