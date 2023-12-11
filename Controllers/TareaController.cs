using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class TareaController : Controller
{
    private readonly TareaRepository tareaRepositorio;
    private readonly ILogger<UsuarioController> _logger;

    public TareaController(ILogger<UsuarioController> logger)
    {
        _logger=logger;
        tareaRepositorio = new TareaRepository();
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult ListarTareas()
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        int.TryParse(HttpContext.Session.GetString("id"), out int id);
        var lista= tareaRepositorio.ListarTareasPorUsuario(id);
        return View(new ListarTareasViewModel(lista));
    }

    [HttpGet]
    public IActionResult ListarTareasTablero(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        var lista= tareaRepositorio.ListarTareasPorTablero(id);
        return View(new ListarTareasViewModel(lista));
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        return View(new TareaViewModel());
    }
    [HttpPost]
    public IActionResult CrearTarea(TareaViewModel tar)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        if (ModelState.IsValid)
        {
            tareaRepositorio.CrearTarea(1, new tarea(tar));
            return RedirectToAction("ListarTareas");
        }
        return View(tar) ;
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        var tarea = tareaRepositorio.ObtenerTarea(id);
        if (tarea!=null)
        {
            return View(new TareaViewModel(tarea));
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Modifica(TareaViewModel tar){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        if (ModelState.IsValid)
        {
            tareaRepositorio.ModificarTarea(tar.Id, new tarea(tar));
            return RedirectToAction("ListarTareas");
        }
        return View(tar);
    }
    [HttpGet]
    public IActionResult EliminarTarea(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        var tarea = tareaRepositorio.ObtenerTarea(id);
        if (tarea!=null)
        {
            return View(new TareaViewModel(tarea));
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Elimina(TareaViewModel tar){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        tareaRepositorio.BorrarTarea(tar.Id);
        return RedirectToAction("ListarTareas");
    }
}