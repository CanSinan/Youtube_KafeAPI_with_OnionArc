using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Mapping;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Application.Services.Concrete;
using KafeAPI.Persistence.AppContext;
using KafeAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var configuration = builder.Configuration.GetConnectionString("DefaultConnection");
    opt.UseSqlServer(configuration);
});

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();

builder.Services.AddAutoMapper(cfg =>
{
    // burada ekstra özel konfigurasyonlar ekleyebiliriz.
}, typeof(GeneralMapping).Assembly);



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

builder.Services.AddEndpointsApiExplorer();
app.MapScalarApiReference(opt =>
{
    opt.Title = "Kafe API v1";
    opt.Theme = ScalarTheme.BluePlanet;
    opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
