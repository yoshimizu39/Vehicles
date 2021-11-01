using Grpc.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;
using Vehicles.API.Models;

namespace Vehicles.API.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;
        private readonly SignInManager<User> _signInManager;

        public UserHelper(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DataContext context,
                          SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }


        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            }
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            string path = user.ImageId;
            FileInfo file = new FileInfo(path);

            if (file.Exists)
            {
                file.Delete();
            }

            return await _userManager.DeleteAsync(user);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.Include(u => u.DocumentType)
                                       .Include(x => x.Vehicles)
                                       .ThenInclude(v => v.VehiclePhotos)
                                       .Include(x => x.Vehicles)
                                       .ThenInclude(v => v.Histories)
                                       .ThenInclude(v => v.Details)
                                       .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _context.Users.Include(x => x.DocumentType)
                                       .Include(x => x.Vehicles)
                                       .ThenInclude(v => v.VehiclePhotos)
                                       .Include(x => x.Vehicles)
                                       .ThenInclude(v => v.Histories)
                                       .ThenInclude(v => v.Details)
                                       .FirstOrDefaultAsync(x => x.Id == id.ToString());
        }

        public async Task<bool> IsUserToRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.Rememberme, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            User usuario = await GetUserAsync(user.Email);
            usuario.LastName = user.LastName;
            usuario.FirstName = user.FirstName;
            usuario.DocumentType = user.DocumentType;
            usuario.Document = user.Document;
            usuario.Address = user.Address;
            usuario.ImageId = user.ImageId;
            usuario.PhoneNumber = user.PhoneNumber;

            return await _userManager.UpdateAsync(usuario);
        }
    }
}
