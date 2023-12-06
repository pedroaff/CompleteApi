using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pedro.App.DTO;
using Pedro.App.Extensions;
using Pedro.Business.Intefaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Pedro.App.Controllers;

[Route("api")]
[ApiController]
public class AuthController : MainController
{

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings;

    public AuthController(INotificador notificador,
                          SignInManager<IdentityUser> signInManager,
                          UserManager<IdentityUser> userManager,
                          IOptions<AppSettings> appSettings) : base(notificador)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
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

            return CustomResponse(GerarJwt());
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

        if (result.Succeeded) return CustomResponse(GerarJwt());

        if (result.IsLockedOut)
        {
            NotificarError("Usuário temporariamente bloqueado");
            return CustomResponse(loginUser);
        }

        NotificarError("Usuário ou senha incorretos");

        return CustomResponse(loginUser);
    }

    private string GerarJwt()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Emissor,
            Audience = _appSettings.ValidoEm,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encoded = tokenHandler.WriteToken(token);

        return encoded;
    }
}
