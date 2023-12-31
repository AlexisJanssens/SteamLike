using System.Text;
using BLL.Interface;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ToolBox.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>(x =>
    new UserRepository(builder.Configuration.GetConnectionString("SteamLikeDb")));
builder.Services.AddScoped<IFriendRepository, FriendRepository>(x =>
    new FriendRepository(builder.Configuration.GetConnectionString("SteamLikeDB")));
builder.Services.AddScoped<IGameRepository, GameRepository>(x=>
    new GameRepository(builder.Configuration.GetConnectionString("SteamLikeDB")));
builder.Services.AddScoped<IPriceRepository, PriceRepository>(x=>
    new PriceRepository(builder.Configuration.GetConnectionString("SteamLikeDB")));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFriendService, FriendService>();


builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IJwtService, JwtService>(x => new JwtService(builder.Configuration["JWT:SecretKey"], builder.Configuration["JWT:ExpirationDays"]));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers().RequireAuthorization();
});

app.Run();