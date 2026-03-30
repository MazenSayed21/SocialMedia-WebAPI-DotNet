using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SOCIALIZE.DTOs;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Helpers;


public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _config;

    public AuthService( IConfiguration config, UserManager<AppUser> userManager, SignInManager<AppUser>signinmanager )
    {
        _userManager = userManager;
        _signInManager=signinmanager;
        _config = config;
    }

    public async Task<bool> RegisterAsync(registerDTO dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.email);
        if (existingUser != null) return false;

        var newUser = new AppUser
        {
            Email = dto.email,
            UserName = dto.userName, 
                                  Role="User"
        };

        var result = await _userManager.CreateAsync(newUser,dto.password );

        if (result.Succeeded)
            return true;

        return false;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return null;

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return CreateToken(user);
        }

        return null;
    }

    private string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role) 
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}