using UniFood.Services;
using UniFood.Utils;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Configurations
ConfigUtil.ApiKey = builder.Configuration.GetValue<string>("ApiKey");
ConfigUtil.ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");
//ConfigUtil.ConnectionString = app.Configuration.GetConnectionString("ConnectionString");

ConfigUtil.JWTAudience = builder.Configuration.GetSection("Jwt").GetValue<string>("Audience");
ConfigUtil.JWTKey = builder.Configuration.GetSection("Jwt").GetValue<string>("Key");
ConfigUtil.JWTIssuer = builder.Configuration.GetSection("Jwt").GetValue<string>("Issuer");

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
//             // Todas las llamadas estan esperando que occura estos atributos
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true, // Tiempo de vida del token
                ValidateIssuerSigningKey = true,
                ValidIssuer = ConfigUtil.JWTIssuer,
                ValidAudience = ConfigUtil.JWTAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigUtil.JWTKey))
            };
       });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<LoginService>();
builder.Services.AddSingleton<UniversitiesService>();
builder.Services.AddSingleton<PlacesService>();
builder.Services.AddSingleton<MenusService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<FavoritesService>();


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
