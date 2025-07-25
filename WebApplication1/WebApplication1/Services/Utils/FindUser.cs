using Microsoft.EntityFrameworkCore;
using WebApplication1.Core.Entities;
using WebApplication1.Enums;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Utils;

public class FindUser
{
    private readonly AppDbContext _dbContext;

    public FindUser(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User?> FindUserByEmail(string email)
    {
        return await _dbContext.User.FirstOrDefaultAsync(user => user.email == email);
    }

    public async Task<User?> FindUserByUserType(string email,TypeUser userType)
    {
        return await _dbContext.User.FirstOrDefaultAsync(user => user.email == email && user.typeUser == userType);
    }

    public async Task<Patient?> FindPatientById(int userId)
    {
        return await _dbContext.Patient.FirstOrDefaultAsync(user => user.Id == userId);
    }

    public async Task<Doctor?> FindDoctorById(int userId)
    {
        return await _dbContext.Doctor.FirstOrDefaultAsync(user => user.Id == userId);
    }

    public async Task<User?> FindUserById(int userId)
    {
        return await _dbContext.User.FindAsync(userId);
    }
}