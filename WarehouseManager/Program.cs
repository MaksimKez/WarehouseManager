using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WarehouseManager.AutoMapperProfilesDtoModel;
using WarehouseManager.BusinessLogic.Auth;
using WarehouseManager.DataAccess;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.DataAccess.Repositories;
using WarehouseManager.Database;
using WarehouseManager.BusinessLogic.AutoMapperProfiles;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Services;
using WarehouseManager.Dtos;
using WarehouseManager.Middlewares;
using EmployeeProfile = WarehouseManager.BusinessLogic.AutoMapperProfiles.EmployeeProfile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddControllers();

builder.Services.AddScoped<IBossRepository, BossRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IShelfRepository, ShelfRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

builder.Services.AddDbContext<ApplicationDatabaseContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(BossProfile));
builder.Services.AddAutoMapper(typeof(EmployeeProfile));
builder.Services.AddAutoMapper(typeof(ItemProfile));
builder.Services.AddAutoMapper(typeof(ShelfProfile));
builder.Services.AddAutoMapper(typeof(TodoProfile));

builder.Services.AddScoped<IBossService, BossService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IShelfService, ShelfService>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddAutoMapper(typeof(BossDtoProfile));
builder.Services.AddAutoMapper(typeof(EmployeeDtoProfile));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
        };
    });

// The code below allows bosses to use employees' endpoints (EmployeePolicy) and their own(BossEmployee),
// but employees can use only endpoints with EmployeePolicy
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("EmployeePolicy", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => (c.Type == "position" && (c.Value == "Employee" || c.Value == "Boss")))))
    .AddPolicy("BossEmployee", policy =>
        policy.RequireClaim("position", "Boss"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.R
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();


app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDatabaseContext>();
    context.EnsureSeedData();
}

app.Run();