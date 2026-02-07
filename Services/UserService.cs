using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Services;

public class UserService
{
    private readonly EventDbContext _context;
    private readonly EncryptionService _encryptionService;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public UserService(EventDbContext context, EncryptionService encryptionService)
    {
        _context = context;
        _encryptionService = encryptionService;
    }

    public async Task<User> CreateAsync(string name, string email, string password)
    {
        var user = new User
        {
            Name = name,
            EmailEncrypted = _encryptionService.Encrypt(email),
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var encryptedEmail = _encryptionService.Encrypt(email);
        return await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.EmailEncrypted == encryptedEmail);
    }

    public async Task<User?> ValidateCredentialsAsync(string email, string password)
    {
        var user = await GetByEmailAsync(email);
        if (user is null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Success ? user : null;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public string DecryptEmail(User user)
    {
        return _encryptionService.Decrypt(user.EmailEncrypted);
    }
}
