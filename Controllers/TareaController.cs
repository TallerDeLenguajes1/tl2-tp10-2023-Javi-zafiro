using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class TareaController : Controller
{
    private readonly ITareaRepository _tareaRepositorio;
    private readonly ILogger<UsuarioController> _logger;

    public TareaController(ILogger<UsuarioController> logger, ITareaRepository tareaRepositorio)
    {
        _logger=logger;
        _tareaRepositorio = tareaRepositorio;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult ListarTareas()
    {
        try
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
            int.TryParse(HttpContext.Session.GetString("id"), out int id);
            var lista= _tareaRepositorio.ListarTareasPorUsuario(id);
            return View(new ListarTareasViewModel(lista));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
        
    }

    [HttpGet]
    public IActionResult ListarTareasTablero(int id)
    {
        try
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
            var lista= _tareaRepositorio.ListarTareasPorTablero(id);
            return View(new ListarTareasViewModel(lista));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        try
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
            return View(new TareaViewModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult CrearTarea(TareaViewModel tar)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        if (ModelState.IsValid)
        {
            _tareaRepositorio.CrearTarea(1, new tarea(tar));
            return RedirectToAction("ListarTareas");
        }
        return View(tar) ;
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        var tarea = _tareaRepositorio.ObtenerTarea(id);
        if (tarea!=null)
        {
            return View(new TareaViewModel(tarea));
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult ModificarTarea(TareaViewModel tar){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        if (ModelState.IsValid)
        {
            _tareaRepositorio.ModificarTarea(tar.Id, new tarea(tar));
            return RedirectToAction("ListarTareas");
        }
        return View(tar);
    }
    [HttpGet]
    public IActionResult EliminarTarea(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        var tarea = _tareaRepositorio.ObtenerTarea(id);
        if (tarea!=null)
        {
            return View(new TareaViewModel(tarea));
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult EliminarTarea(TareaViewModel tar){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        _tareaRepositorio.BorrarTarea(tar.Id);
        return RedirectToAction("ListarTareas");
    }

    public IActionResult Error(){
        return View(new ErrorViewModel());
    }
}

/*
        try
        {
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
*/