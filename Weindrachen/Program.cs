using System.Reflection;
using Weindrachen.Configuration;
using Weindrachen.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(options => 
{
    options.Version = "v1";
    options.Title = "Weindrachen";
    options.Description = "Weindrachen, a Web API to manage wines.";
});

# region [DbContext Registrer]

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("Default"),
        new MySqlServerVersion(new Version(8, 4, 0)));
});

# endregion

# region [Repository Register]

builder.Services.AddRepositories();

# endregion

# region [MediatR]

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
);

# endregion

# region [Validators]

builder.Services.AddValidators();

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