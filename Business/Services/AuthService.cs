using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class AuthService(SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager) : IAuthService
{
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<bool> LoginAsync(MemberLoginForm loginForm)
    {
      var result =  await _signInManager.PasswordSignInAsync(loginForm.Email, loginForm.Password, false, false);
      return result.Succeeded;
    }
    
    public async Task<bool> SignUpAsync(MemberSignUpForm signUpForm)
    {
        var memberEntity = new MemberEntity
        {
            UserName = signUpForm.Email,
            FirstName = signUpForm.FirstName,
            LastName = signUpForm.LastName,
            Email = signUpForm.Email,
            PhoneNumber = signUpForm.Phone
        };
        
        var result = await _userManager.CreateAsync(memberEntity, signUpForm.Password);
        
        /*if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error: {error.Code} - {error.Description}");
            }
        }*/
        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}