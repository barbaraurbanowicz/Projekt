using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SpendingTrackerAPI.Database;
using SpendingTrackerAPI.DTOModels;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Exceptions;

namespace SpendingTrackerAPI.Services;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthSettings _authSettings;

    public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher,
        AuthSettings authSettings)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _authSettings = authSettings;
    }
    
    public void Register(CreateUserDTO createUserDto)
    {
        User user = new User()
        {
            Email = createUserDto.Email,
            RoleId = createUserDto.RoleId,
        };
        var hashPassword = _passwordHasher.HashPassword(user, createUserDto.Password);
        user.PasswordHash = hashPassword;
        _context.Users.Add(user);
        _context.SaveChanges();
    }
    
    public string Login(LoginUserDTO loginUserDto)
    {
        var user = _context.Users
            .Include(u => u.Role)
            .FirstOrDefault(x => x.Email == loginUserDto.Email);
        if (user is null)
        {
            throw new BadRequestException("Invalid email or password");
        }
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException("Invalid email or password");
        }
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.Email}"),
            new Claim(ClaimTypes.Role, user.Role.Name),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authSettings.JwtExpireDays);
        var token = new JwtSecurityToken(_authSettings.JwtIssuer,
            _authSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}