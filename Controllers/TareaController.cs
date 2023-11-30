using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class TareaController : Controller
{
    private readonly TareaRepository tareaRepositorio;

    public TareaController()
    {
        tareaRepositorio = new TareaRepository();
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult ListarTareas()
    {
        var lista= tareaRepositorio.ListarTodasTareas();
        return View(lista);
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        return View(new tarea());
    }
    [HttpPost]
    public IActionResult CrearTarea(tarea tar)
    {
        if (ModelState.IsValid)
        {
            tareaRepositorio.CrearTarea(1,tar);
            return RedirectToAction("ListarTareas");
        }
        return View(tar) ;
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        var tarea = tareaRepositorio.ObtenerTarea(id);
        if (tarea!=null)
        {
            return View(tarea);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Modifica(tarea tar){
        if (ModelState.IsValid)
        {
            tareaRepositorio.ModificarTarea(tar.Id, tar);
            return RedirectToAction("ListarTareas");
        }
        return View(tar);
    }
    [HttpGet]
    public IActionResult EliminarTarea(int id)
    {
        var tarea = tareaRepositorio.ObtenerTarea(id);
        if (tarea!=null)
        {
            return View(tarea);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Elimina(tarea tar){
        tareaRepositorio.BorrarTarea(tar.Id);
        return RedirectToAction("ListarTareas");
    }
}