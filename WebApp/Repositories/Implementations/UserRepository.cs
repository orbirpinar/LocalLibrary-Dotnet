using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories.Implementations
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly UserManager<User> _userManager;


        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> CreateAsync(User user,string password)
        {
           return await _userManager.CreateAsync(user,password);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var userToBeUpdate = await _userManager.FindByIdAsync(user.Id);
            if (userToBeUpdate is null) return false;
            userToBeUpdate.FirstName = user.FirstName;
            userToBeUpdate.LastName = user.LastName;
            userToBeUpdate.UserName = user.UserName;
            userToBeUpdate.Email = user.Email;
            userToBeUpdate.PhoneNumber = user.PhoneNumber;
            await _userManager.UpdateAsync(userToBeUpdate);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var userToBeDelete = await _userManager.FindByIdAsync(id);
            if (userToBeDelete is null) return false;
            await _userManager.DeleteAsync(userToBeDelete);
            return true;
        }

        public async Task<IdentityResult> AssignRoleAsync(User user, string roleName)
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IList<string>> GetRoleNamesAsync(User? user)
        {
            if (user is not null)
            {
                return await _userManager.GetRolesAsync(user);
            }

            return new List<string>();
        }

        public async Task<User> GetLoggedInUser(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }


        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            _userManager.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}