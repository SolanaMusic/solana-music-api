using Microsoft.EntityFrameworkCore;
using solana_music_api;
using solana_music_api.SeedData;
using SolanaMusicApi.Application;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
if (connection == null)
    throw new Exception("Connection not set");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection));

builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.ConfigureIdentity();

builder.Services.AddControllers();
builder.Services.ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.CreateDefaultRolesAsync();
await app.CreateDefaultUsersAsync();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
