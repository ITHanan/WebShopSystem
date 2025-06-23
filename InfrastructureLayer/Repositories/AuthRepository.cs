using ApplicationLayer.Interfaces;
using ApplicationLayer.Interfaces5;
using DomainLayer.Common;
using DomainLayer.Models;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class AuthRepository : IAuthRepository

    {
        private readonly WebShopSystemDbContext _context;

        public AuthRepository(WebShopSystemDbContext context)
        {
            _context = context;
        }

        //Add a new user to the database
        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        //check if email is aleady register

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email!.ToLower() == email.ToLower());
        }
        //Get user by ID

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(User => User.UserName!.ToLower() == username!.ToLower());
        }
        //save chnges to data 
        public async Task<OperationResult<bool>> SaveChangesAsync() 
        {
            try
            {

                await _context.SaveChangesAsync();
                return OperationResult<bool>.Success(true);
            
            }
            catch (Exception ex) 
            {
                //Handel umexception erors 
                return OperationResult<bool>.Failure($"Saving changes failed{ex.Message}");
            }
        
        }
        
    }
}
