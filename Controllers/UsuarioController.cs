using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class UsuarioController : Controller
{
    private readonly IUsuarioRepository _usuarioRepositorio;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepositorio)
    {
        _logger = logger;
        _usuarioRepositorio = usuarioRepositorio;
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
            var lista= _usuarioRepositorio.ListarUsuarios();
            if (HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString())
            {
                return View(new ListaUsuariosViewModel(lista, false));
            }else
            {
                return View(new ListaUsuariosViewModel(lista, true));
            }
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
                _usuarioRepositorio.CrearUsuario(nuevo);
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
            var usu = _usuarioRepositorio.ObtenerUsuario(id);
            var usuVM= new UsuarioViewModel();
            usuVM.NombreDeUsuario=usu.NombreDeUsuario;
            usuVM.Contrasenia=usu.Contrasenia;
            usuVM.Tipo=usu.Tipo;
            usuVM.Id=usu.Id;
            return View(usuVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Index");
        }
        
    }

    [HttpPost]
    public IActionResult ModificarUsuario(UsuarioViewModel usu){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        //if(!ModelState.IsValid) return RedirectToAction("Index");
        var usuarioMod=new usuario(usu);
            try
            {
                _usuarioRepositorio.ModificarUsuario(usu.Id, usuarioMod);
                return RedirectToAction("ListarUsuarios");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
    }
    [HttpGet]
    public IActionResult EliminarUsuario(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        try
        {
            var usuario = _usuarioRepositorio.ObtenerUsuario(id);
            return View(new UsuarioViewModel(usuario));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult EliminarUsuario(UsuarioViewModel usu){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        try
        {
            _usuarioRepositorio.BorrarUsuario(usu.Id);
            return RedirectToAction("EliminarTableroUsuario", "Tablero", new {id = usu.Id});
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