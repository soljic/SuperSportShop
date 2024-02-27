using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Repositories.User
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetUsers();

        Task<int> GetUsersCount();

        Task<ApplicationUser> GetUser(int id);

        Task<ApplicationUser> GetUserWithCredentials(string username, string password);

        Task<ApplicationUser> GetUserApplication(int applicationId);

        Task InsertUser(ApplicationUser user);

        Task UpdateUser(ApplicationUser user);

        Task DeleteUser(int id);
    }
}