using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CinemaSolutionApi.Dtos.Admin;
using CinemaSolutionApi.Dtos.User;
using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Helpers;
using CinemaSolutionApi.Mapping;
using CinemaSolutionApi.Data;

namespace CinemaSolutionApi.Services;

public class AdminService
{
    private readonly CinemaSolutionContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public AdminService(CinemaSolutionContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _signInManager = signInManager;
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

    public async Task<bool> CreateUser(CreateUserDto newUser)
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

        return true;
    }
    public async Task<User> ModifyUser(string id, CreateUserDto newValues)
    {
        var user = await GetUser(id);
        user.UserName = newValues.Username;
        user.Email = newValues.Email;
        user.Name = newValues.Name;
        user.LastName = newValues.LastName;
        user.Email = newValues.Email;
        //nuevos valores
        var resultModifyUser = await _userManager.UpdateAsync(user);
        if (!resultModifyUser.Succeeded)
        {
            var formattedErrors = IdentityErrorHelper.FormatErrors(resultModifyUser.Errors);
            throw new IdentityValidationEx(formattedErrors);
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resultResetPassword = await _userManager.ResetPasswordAsync(user, token, newValues.Password);
        //ver si es mail es correcto porq la base de datos lo guardo
        // "email": "string@modify1"

        if (!resultResetPassword.Succeeded)
        {
            var formattedErrors = IdentityErrorHelper.FormatErrors(resultResetPassword.Errors);
            throw new IdentityValidationEx(formattedErrors);
        }
        //asignar nuevos roles
        var roles = await _userManager.GetRolesAsync(user);
        //roles son los roles actuales, 
        if (roles[0] != newValues.Role)
        {
            var resultRemoveRole = await _userManager.RemoveFromRoleAsync(user, roles[0]);
            if (!resultRemoveRole.Succeeded)
            {
                var formattedErrors = IdentityErrorHelper.FormatErrors(resultRemoveRole.Errors);
                throw new IdentityValidationEx(formattedErrors);
            }
            var resultAddRole = await _userManager.AddToRoleAsync(user, newValues.Role);
            if (!resultAddRole.Succeeded)
            {
                var formattedErrors = IdentityErrorHelper.FormatErrors(resultAddRole.Errors);
                throw new IdentityValidationEx(formattedErrors);
            }
        }
        return user;
    }

    public async Task DeleteUser(string id)
    {
        var user = await GetUser(id);
        await _userManager.DeleteAsync(user);
    }

    public async Task<User> GetUser(string id)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");
    }
}