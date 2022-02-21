using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories.Implementations
{
    public class RoleRepository:IRoleRepository,IDisposable
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<IdentityRole>> GetAllAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityRole?> GetByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> CreateAsync(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<bool> UpdateAsync(IdentityRole role)
        {
            var roleToBeUpdate = await _roleManager.FindByIdAsync(role.Id);
            if (roleToBeUpdate is null) return false;
            roleToBeUpdate.Name = role.Name;
            await _roleManager.UpdateAsync(roleToBeUpdate);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var roleToBeDelete = await _roleManager.FindByIdAsync(id);
            if (roleToBeDelete is null) return false;
            await _roleManager.DeleteAsync(roleToBeDelete);
            return true;
        }

        private bool  _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _roleManager.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            _roleManager.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}