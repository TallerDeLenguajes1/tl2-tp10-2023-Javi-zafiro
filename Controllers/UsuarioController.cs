using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class UsuarioController : Controller
{
    private readonly UsuarioRepository usuarioRepositorio;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepositorio = new UsuarioRepository();
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult ListarUsuarios()
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        try
        {
            var lista= usuarioRepositorio.ListarUsuarios();
            return View(new ListaUsuariosViewModel(lista));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
        
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        return View(new UsuarioViewModel());
    }
    [HttpPost]
    public IActionResult CrearUsuario(UsuarioViewModel usu)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        if (ModelState.IsValid)
        {
            var nuevo = new usuario(usu);
            try
            {
                usuarioRepositorio.CrearUsuario(nuevo);
                return RedirectToAction("ListarUsuarios");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
            
        }
        return View(usu) ;
    }
    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        try
        {
            var usuario = usuarioRepositorio.ObtenerUsuario(id);
            return View(new UsuarioViewModel(usuario));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
        
    }

    [HttpPost]
    public IActionResult Modifica(UsuarioViewModel usu){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        if (ModelState.IsValid)
        {
            try
            {
                usuarioRepositorio.ModificarUsuario(usu.Id, new usuario(usu));
                return RedirectToAction("ListarUsuarios");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
            
        }
        return View(usu);
    }
    [HttpGet]
    public IActionResult EliminarUsuario(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        try
        {
            var usuario = usuarioRepositorio.ObtenerUsuario(id);
            return View(new UsuarioViewModel(usuario));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult Elimina(UsuarioViewModel usu){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        try
        {
            usuarioRepositorio.BorrarUsuario(usu.Id);
            return RedirectToAction("ListarUsuarios");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    public IActionResult Error(){
        return View(new ErrorViewModel());
    }
}