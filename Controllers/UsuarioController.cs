using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class UsuarioController : Controller
{
    private readonly UsuarioRepository usuarioRepositorio;

    public UsuarioController()
    {
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
        var lista= usuarioRepositorio.ListarUsuarios();
        return View(lista);
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        return View(new usuario());
    }
    [HttpPost]
    public IActionResult CrearUsuario(usuario usu)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        if (ModelState.IsValid)
        {
            usuarioRepositorio.CrearUsuario(usu);
            return RedirectToAction("ListarUsuarios");
        }
        return View(usu) ;
    }
    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        var usuario = usuarioRepositorio.ObtenerUsuario(id);
        if (usuario!=null)
        {
            return View(usuario);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Modifica(usuario usu){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        if (ModelState.IsValid)
        {
            usuarioRepositorio.ModificarUsuario(usu.Id, usu);
            return RedirectToAction("ListarUsuarios");
        }
        return View(usu);
    }
    [HttpGet]
    public IActionResult EliminarUsuario(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        var usuario = usuarioRepositorio.ObtenerUsuario(id);
        if (usuario!=null)
        {
            return View(usuario);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Elimina(usuario usu){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        if(HttpContext.Session.GetString("rol") != TiposUsuario.Administrador.ToString()) return RedirectToAction("Error");
        usuarioRepositorio.BorrarUsuario(usu.Id);
        return RedirectToAction("ListarUsuarios");
    }
}