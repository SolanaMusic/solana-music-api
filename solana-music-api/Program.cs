using Microsoft.EntityFrameworkCore;
using solana_music_api;
using solana_music_api.Extensions;
using SolanaMusicApi.Extensions;
using System.Text.Json.Serialization;
using SolanaMusicApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
if (connection == null)
    throw new Exception("Connection not set");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureGeneral();
builder.AddJwtAuthentication();

builder.Services.ConfigureSwagger();
builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

await app.CreateDefaultRolesAsync();
await app.CreateDefaultUsersAsync();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
