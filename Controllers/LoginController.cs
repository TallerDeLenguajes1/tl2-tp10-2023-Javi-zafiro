using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository _usuarioRepositorio;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult ComprobarUsuario(LoginViewModel usu){
        try
        {
            //if (!ModelState.IsValid) return RedirectToAction("Index");
            var log = _usuarioRepositorio.ObtenerUsuarioLogin(usu.Nombre, usu.Contrasenia);
            if (log == null) 
            {
                _logger.LogWarning("Intento de acceso invalido - Usuario:" + usu.Nombre + " Clave ingresada: " + usu.Contrasenia);
                var loginVMMensaje = new LoginViewModel
                {
                    MensajeError = "Usuario o Contraseña no válido"
                };
                return View("Index",loginVMMensaje);
            }
            LoguearUsuario(log);
            _logger.LogInformation($"El usuario {usu.Nombre} ingreso correctamente");
            return RedirectToRoute(new{controller="Home", action="Index"});
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Error: {ex} Intento de acceso invalido - Usuario: {usu.Nombre} - Clave ingresada: {usu.Contrasenia}");
            return RedirectToAction("Index");
        }
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
        _logger.LogInformation($"El usuario cerro sesion");
        return RedirectToAction("Index");
    }

}
