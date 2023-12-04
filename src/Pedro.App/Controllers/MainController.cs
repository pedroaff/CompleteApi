using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pedro.Business.Intefaces;
using Pedro.Business.Notificacoes;

namespace Pedro.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class MainController : ControllerBase
{
    protected readonly INotificador _notificador;

    protected MainController(INotificador notificador)
    {
        _notificador = notificador;
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotificarErroModelInvalida(modelState);

        return CustomResponse();
    }

    protected ActionResult CustomResponse(object? result = null)
    {
        if (OperacaoValida()) return Ok(new
        {
            success = true,
            data = result
        });

        return BadRequest(new
        {
            success = false,
            errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
        });
    }

    protected bool OperacaoValida()
    {
        return !_notificador.TemNotificacao();
    }

    protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            var errorMsg = error.Exception is null ? error.ErrorMessage : error.Exception.Message;
            NotificarError(errorMsg);
        }
    }

    protected void NotificarError(string message)
    {
        _notificador.Handle(new Notificacao(message));
    }
}
