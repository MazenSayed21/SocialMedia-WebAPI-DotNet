using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SOCIALIZE.Data;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;
using SOCIALIZE.Repositories;
using SOCIALIZE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString)
    ;
});

builder.Services.AddIdentityCore<AppUser>(options =>
{
    options.Password.RequireDigit = false; // Set these based on your project needs
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddSignInManager<SignInManager<AppUser>>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<ICommentRepo, CommentRepo>();
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<INotificationRepo, NotificationRepo>();
builder.Services.AddScoped<IReactionRepo, ReactionRepo>();
builder.Services.AddScoped<IFriendShipRepo, FriendShipRepo>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IFriendshipService, FriendShipService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IReactionService, ReactionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ORDER MATTERS HERE:
app.UseAuthentication(); // "Who are you?"
app.UseAuthorization();  // "Are you allowed to be here?"

app.MapControllers();

app.Run();