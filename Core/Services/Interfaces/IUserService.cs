using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
         Task<List<ApplicationUser>> GetUsers();
    }
}