using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Mapping;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Application.Services.Concrete;
using KafeAPI.Persistence.AppContext;
using KafeAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using FluentValidation;
using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Dtos.OrderItemDtos;
using KafeAPI.Application.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using KafeAPI.Persistence.AppContext.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;
using KafeAPI.Persistence.Middlewares;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var configuration = builder.Configuration.GetConnectionString("DefaultConnection");
    opt.UseSqlServer(configuration);
});
builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
{
    var configuration = builder.Configuration.GetConnectionString("DefaultConnection");
    opt.UseSqlServer(configuration);
});
builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<TokenHelpers>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(cfg =>
{
    // burada ekstra özel konfigurasyonlar ekleyebiliriz.
}, typeof(GeneralMapping).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateMenuItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTableDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateTableDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderItemDto>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();


builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(
    builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    
    
    //serilog config


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);
builder.Host.UseSerilog();

builder.Services.AddHttpContextAccessor();

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

app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SerilogMiddleware>();

app.MapControllers();

app.Run();
