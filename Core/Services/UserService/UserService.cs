using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using Domain.Interfaces.Repositories.User;
using Domain.Models;
using Microsoft.AspNet.Identity;
using Unity;

namespace Core.Service.UserService
{
    public class UserService : IUserService, IUserStore<ApplicationUser>, IUserRoleStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
    {
        
        private readonly IUserRepository _userRepository;
        
        [InjectionConstructor]
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserService()
        {
        }
        
        // Implementacija metoda sučelja IUserStore, IUserRoleStore, IUserPasswordStore, IUserEmailStore

        // Primjer implementacije metoda sučelja IUserStore<ApplicationUser>
        public Task CreateAsync(ApplicationUser user)
        {
            // Implementacija stvaranja korisnika u bazi podataka
            return Task.CompletedTask; // Placeholder povratna vrijednost
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            // Implementacija ažuriranja korisnika u bazi podataka
            return Task.CompletedTask; // Placeholder povratna vrijednost
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            // Implementacija brisanja korisnika iz baze podataka
            return Task.CompletedTask; // Placeholder povratna vrijednost
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            // Implementacija pronalaska korisnika po ID-u u bazi podataka
            return Task.FromResult<ApplicationUser>(null); // Placeholder povratna vrijednost
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            // Implementacija pronalaska korisnika po korisničkom imenu u bazi podataka
            return Task.FromResult<ApplicationUser>(null); // Placeholder povratna vrijednost
        }

        public Task<List<ApplicationUser>> GetUsersAsync()
        {
            return _userRepository.GetUsers();
        }
        // Ostale metode sučelja IUserStore<ApplicationUser> bi se također implementirale na sličan način
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task SetEmailAsync(ApplicationUser user, string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetEmailAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }
    }
}