using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly UsuarioRepository usuarioRepositorio;

    public LoginController(ILogger<LoginController> logger )
    {
        usuarioRepositorio = new UsuarioRepository();
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ComprobarUsuario(login usu){
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var log = usuarioRepositorio.ObtenerUsuarioLogin(usu.Nombre, usu.Contrasenia);
        if (log.NombreDeUsuario!=null)
        {
            LoguearUsuario(log);
        }else
        {
            _logger.LogWarning("usuario o contraseña incorrecto");
            return RedirectToRoute(new{controller="Usuario", action="Index"});
        }
        return RedirectToRoute(new{controller="Usuario", action="ListarUsuarios"});
    }

    private void LoguearUsuario(usuario usua){
        HttpContext.Session.SetString("id", usua.Id.ToString());
        HttpContext.Session.SetString("usuario", usua.NombreDeUsuario);
        HttpContext.Session.SetString("rol", usua.Tipo.ToString());
    }

    [HttpPost] 
    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

}
