using eMart.Config;
using eMart_Repository.Migrations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
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
await context.Database.MigrateAsync();   
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSession();
app.MapControllers();

app.Run();
