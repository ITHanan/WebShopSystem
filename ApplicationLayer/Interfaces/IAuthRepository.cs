using DomainLayer.Common;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces5
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);

        // Checks if email is already registered in the system
        Task<bool> EmailExistsAsync(string email);

        // Adds a new user to the database (registration)
        Task CreateUserAsync(User user);

        // Saves pending changes to the database and wraps the result
        Task<OperationResult<bool>> SaveChangesAsync();
    }
}
