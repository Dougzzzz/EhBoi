using EhBoi.Domain.Migrations;
using EhBoi.Infra;
using EhBoi.Infra.Data;
using EhBoi.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EhBoiDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddScoped<ServicoDeMigrations<EhBoiDbContext>>();
builder.Services.AddControllers();
builder.Services.AddSingleton<IStatusRepository, StatusRepository>
                (sp => new StatusRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var migrationService = services.GetRequiredService<ServicoDeMigrations<EhBoiDbContext>>();
    await migrationService.MigrateAsync();
}

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
