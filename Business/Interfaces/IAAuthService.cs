using Business.Models;

namespace Business.Interfaces;

public interface IAAuthService
{ 
    Task<bool> LoginAsync(MemberLoginForm loginForm); 
    Task LogoutAsync();
    Task<bool> SignUpAsync(MemberSignUpForm signUpForm);  
}