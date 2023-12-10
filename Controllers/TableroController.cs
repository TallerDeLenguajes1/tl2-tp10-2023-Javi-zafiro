using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class TableroController : Controller
{
    private readonly TableroRepository tableroRepositorio;
    private readonly ILogger<UsuarioController> _logger;

    public TableroController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        tableroRepositorio = new TableroRepository();
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult ListarTableros()
    {
        try
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
                return View(new ListaTodosTablerosViewModel(lista));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        try
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
            return View(new TableroViewModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
        
    }
    [HttpPost]
    public IActionResult CrearTablero(TableroViewModel tab)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        if (ModelState.IsValid)
        {
            try
            {
                tableroRepositorio.CrearTablero(new tablero(tab));
                return RedirectToAction("ListarTableros");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }
        return View(tab) ;
    }
    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            var tablero = tableroRepositorio.ObtenerTablero(id);
            return View(new TableroViewModel(tablero));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult Modifica(TableroViewModel tab){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        if (ModelState.IsValid)
        {
            try
            {
                tableroRepositorio.ModificarTablero(tab.Id, new tablero(tab));
                return RedirectToAction("ListarTableros");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }
        return View(tab);
    }
    [HttpGet]
    public IActionResult EliminarTablero(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            var tablero = tableroRepositorio.ObtenerTablero(id);
            return View(new TableroViewModel(tablero));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult Elimina(TableroViewModel tab){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            tableroRepositorio.BorrarTablero(tab.Id);
            return RedirectToAction("ListarTableros");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
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