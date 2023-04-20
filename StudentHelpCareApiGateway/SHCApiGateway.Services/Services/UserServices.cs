﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SHCApiGateway.Data.Entity;
using SHCApiGateway.Data.Model;
using SHCApiGateway.Services.Iservices;
using SHCApiGateway.ViewModel.User;

namespace SHCApiGateway.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<UserServices> _logger;

        public UserServices(
        UserManager<User> userManager,
        ILogger<UserServices> logger,
        RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
        }


        public async Task<string> CreateUser(UserViewModel user)
        {
            string isSuccess = "false";

            try
            {
                if (user != null && !string.IsNullOrEmpty(user.UserName) 
                    && !string.IsNullOrEmpty(user.Password))
                {
                    var userData = new User
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0
                    };

                    var result = await _userManager.CreateAsync(userData, user.Password);

                    if (result.Succeeded)
                    {
                        isSuccess = "true";
                    }
                    else
                    {
                        if (result.Errors.Any())
                        {
                            isSuccess = string.Join(" ", result.Errors.Select(x => x.Description));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login error:", ex);
            }

            return isSuccess;
        }

        public async Task<string> CreateRole(RoleModel role)
        {
            string isSuccess = "false";

            try
            {
                if (role == null)
                {
                    return isSuccess;
                }

                var roleData = new Role()
                {
                    Name = role.Name,
                    NormalizedName = role.NormalizedName,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };

                var result = await _roleManager.CreateAsync(roleData);

                if (result.Succeeded)
                {
                    isSuccess = "true";
                }
                else
                {
                    if (result.Errors.Any())
                    {
                        isSuccess = string.Join(" ", result.Errors.Select(e => e.Description));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Role error:", ex);
            }

            return isSuccess;
        }
    }
}
