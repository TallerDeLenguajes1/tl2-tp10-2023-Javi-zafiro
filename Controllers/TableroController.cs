using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class TableroController : Controller
{
    private readonly TableroRepository tableroRepositorio;

    public TableroController()
    {
        tableroRepositorio = new TableroRepository();
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult ListarTableros()
    {
        var lista= tableroRepositorio.ListarTableros();
        return View(lista);
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        return View(new tablero());
    }
    [HttpPost]
    public IActionResult CrearTablero(tablero tab)
    {
        if (ModelState.IsValid)
        {
            tableroRepositorio.CrearTablero(tab);
            return RedirectToAction("ListarTableros");
        }
        return View(tab) ;
    }
    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        var tablero = tableroRepositorio.ObtenerTablero(id);
        if (tablero!=null)
        {
            return View(tablero);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Modifica(tablero tab){
        if (ModelState.IsValid)
        {
            tableroRepositorio.ModificarTablero(tab.Id, tab);
            return RedirectToAction("ListarTableros");
        }
        return View(tab);
    }
    [HttpGet]
    public IActionResult EliminarTablero(int id)
    {
        var tablero = tableroRepositorio.ObtenerTablero(id);
        if (tablero!=null)
        {
            return View(tablero);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Elimina(usuario usu){
        tableroRepositorio.BorrarTablero(usu.Id);
        return RedirectToAction("ListarTableros");
    }
}