using System.Reflection;
using FluentValidation;
using Weindrachen.Configuration;
using Weindrachen.DTOs.Brand;
using Weindrachen.DTOs.Grape;
using Weindrachen.DTOs.Wine;
using Weindrachen.Infrastructure.Data;
using Weindrachen.Infrastructure.Repositories;
using Weindrachen.Infrastructure.Validators;
using Weindrachen.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("Default"),
        new MySqlServerVersion(new Version(8, 4, 0)));
});

# region [Repository Register]

builder.Services.AddRepositories();

# endregion

# region [MediatR]

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
);

# endregion

# region [Validators]

builder.Services.AddScoped<IValidator<BrandInput>, BrandValidator>();
builder.Services.AddScoped<IValidator<GrapeInput>, GrapeValidator>();
builder.Services.AddScoped<IValidator<WineInput>, WineValidator>();

# endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();