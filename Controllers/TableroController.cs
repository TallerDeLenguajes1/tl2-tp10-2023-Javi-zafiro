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
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))){
            return RedirectToRoute(new{controller= "Login", action="index"});
        }else
        {
            List<tablero> lista;
            if (HttpContext.Session.GetString("rol")==TiposUsuario.Administrador.ToString())
            {
                lista= tableroRepositorio.ListarTableros();
            }else
            {
                int idusu;
                var id=HttpContext.Session.GetString("id");
                int.TryParse(id, out idusu);
                lista = tableroRepositorio.ListarTablerosDeUsuario(idusu);
            }
            return View(lista);
        }
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        return View(new tablero());
    }
    [HttpPost]
    public IActionResult CrearTablero(tablero tab)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
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
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        var tablero = tableroRepositorio.ObtenerTablero(id);
        if (tablero!=null)
        {
            return View(tablero);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Modifica(tablero tab){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
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
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        var tablero = tableroRepositorio.ObtenerTablero(id);
        if (tablero!=null)
        {
            return View(tablero);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Elimina(tablero tab){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        tableroRepositorio.BorrarTablero(tab.Id);
        return RedirectToAction("ListarTableros");
    }
}