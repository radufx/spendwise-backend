using Microsoft.EntityFrameworkCore;
using spendwise.DataAccess;
using spendwise.Business.Interfaces;
using spendwise.Business;
using spendwise.DataAccess.Repositories;
using spendwise.DataAccess.Entities;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("SpendWiseContext");
builder.Services.AddDbContext<SpendWiseContext>(options =>
{ 
    options.UseSqlServer(connectionString);
});

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IRepository<Category>, CategoryRepository>();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IRepository<Product>, ProductRepository>();

builder.Services.AddTransient<IReceiptService, ReceiptService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

