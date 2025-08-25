using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CinemaSolutionApi.Dtos.Admin;
using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Helpers;
using CinemaSolutionApi.Mapping;
using CinemaSolutionApi.Data;

namespace CinemaSolutionApi.Services;

public class AdminService
{
    private readonly CinemaSolutionContext _dbContext;
    private readonly UserManager<User> _userManager;
    public AdminService(CinemaSolutionContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<User> GetUser(string id)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");
    }
    public async Task<List<GetUserDto>> GetUsers()
    {
        var users = await _userManager.Users
        .Select(u => u)
        .ToListAsync();

        var listUsers = new List<GetUserDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            listUsers.Add(user.ToGetUser(roles));
        }
        return listUsers;
    }

    public async Task CreateUser(CreateUserDto newUser)
    {
        var user = new User()
        {
            Name = newUser.Name,
            LastName = newUser.LastName,
            UserName = newUser.Username,
            Email = newUser.Email
        };

        var result = await _userManager.CreateAsync(user, newUser.Password);

        if (!result.Succeeded)
        {
            var formattedErrors = IdentityErrorHelper.FormatErrors(result.Errors);
            throw new IdentityValidationEx(formattedErrors);
        }

        await _userManager.AddToRoleAsync(user, newUser.Role);

    }
    public async Task<User> ModifyUser(string id, CreateUserDto newValues)
    {
        var user = await GetUser(id);

        user.UserName = newValues.Username;
        user.Email = newValues.Email;
        user.Name = newValues.Name;
        user.LastName = newValues.LastName;
        user.Email = newValues.Email;

        await ValidateModifyUser(user, newValues);

        return user;
    }

    public async Task DeleteUser(string id)
    {
        var user = await GetUser(id);
        await _userManager.DeleteAsync(user);
    }



    public async Task ValidateModifyUser(User user, CreateUserDto newValues)
    {
        var resultModifyUser = await _userManager.UpdateAsync(user);
        if (!resultModifyUser.Succeeded) throw new IdentityValidationEx(IdentityErrorHelper.FormatErrors(resultModifyUser.Errors));

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resultResetPassword = await _userManager.ResetPasswordAsync(user, token, newValues.Password);

        if (!resultResetPassword.Succeeded) throw new IdentityValidationEx(IdentityErrorHelper.FormatErrors(resultResetPassword.Errors));

        var roles = await _userManager.GetRolesAsync(user);
        if (roles[0] != newValues.Role)
        {
            var resultRemoveRole = await _userManager.RemoveFromRoleAsync(user, roles[0]);
            if (!resultRemoveRole.Succeeded) throw new IdentityValidationEx(IdentityErrorHelper.FormatErrors(resultRemoveRole.Errors));

            var resultAddRole = await _userManager.AddToRoleAsync(user, newValues.Role);
            if (!resultAddRole.Succeeded) throw new IdentityValidationEx(IdentityErrorHelper.FormatErrors(resultAddRole.Errors));
        }
    }
}