using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pedro.App.DTO;
using Pedro.Business.Intefaces;

namespace Pedro.App.Controllers;

[Route("api")]
[ApiController]
public class AuthController : MainController
{

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(INotificador notificador,
                          SignInManager<IdentityUser> signInManager,
                          UserManager<IdentityUser> userManager) : base(notificador)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("criar-conta")]
    public async Task<ActionResult> Registar(RegisterUserDto registerUser)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        IdentityUser user = new()
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);

            return CustomResponse(registerUser);
        }

        foreach (var error in result.Errors)
        {
            NotificarError(error.Description);
        }

        return CustomResponse(registerUser);
    }

    [HttpPost("entrar")]
    public async Task<ActionResult> Login(LoginUserDto loginUser)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email,
                                                              loginUser.Password,
                                                              false,
                                                              true);

        if (result.Succeeded) return CustomResponse(loginUser);

        if (result.IsLockedOut)
        {
            NotificarError("Usuário temporariamente bloqueado");
            return CustomResponse(loginUser);
        }

        NotificarError("Usuário ou senha incorretos");

        return CustomResponse(loginUser);
    }
}
