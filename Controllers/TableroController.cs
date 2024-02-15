using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class TableroController : Controller
{
    private readonly ITableroRepository _tableroRepositorio;
    private readonly ITareaRepository _tareaRepositorio;
    private readonly ILogger<UsuarioController> _logger;

    public TableroController(ILogger<UsuarioController> logger, ITableroRepository tableroRepositorio, ITareaRepository tareaRepositorio)
    {
        _logger = logger;
        _tableroRepositorio = tableroRepositorio;
        _tareaRepositorio = tareaRepositorio;
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
                
                if (HttpContext.Session.GetString("rol")==TiposUsuario.Administrador.ToString())
                {
                    List<tablero> lista;
                    lista= _tableroRepositorio.ListarTableros();
                    return View(new ListaTablerosViewModel(lista));
                }else
                {
                    List<tablero> listaPropios;
                    List<tablero> listaNoPropios;
                    List<tarea> list;
                    int idusu;
                    var id=HttpContext.Session.GetString("id");
                    int.TryParse(id, out idusu);
                    listaPropios = _tableroRepositorio.ListarTablerosDeUsuario(idusu);
                    list=_tareaRepositorio.ListarTareasPorUsuario(idusu);
                    if (list.Count>0)
                    {
                        listaNoPropios=_tableroRepositorio.ListarTablerosPorTareas(list);
                        listaNoPropios.RemoveAll(item => listaPropios.Exists(t => t.Id == item.Id));
                        return View(new ListaTablerosViewModel(listaPropios, listaNoPropios));
                    }else{
                        return View(new ListaTablerosViewModel(listaPropios));
                    }
                    
                }
                
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("ErrorLista");
        }
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        try
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
            int id;
            int.TryParse(HttpContext.Session.GetString("id"), out id);
            return View(new CrearTableroViewModel(id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
        
    }
    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel tab)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        if (ModelState.IsValid)
        {
            try
            {
                _tableroRepositorio.CrearTablero(new tablero(tab));
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
            var tablero = _tableroRepositorio.ObtenerTablero(id);
            return View(new TableroViewModel(tablero));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult ModificarTablero(TableroViewModel tab){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        if (ModelState.IsValid)
        {
            try
            {
                _tableroRepositorio.ModificarTablero(tab.Id, new tablero(tab));
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
            var tablero = _tableroRepositorio.ObtenerTablero(id);
            return View(new TableroViewModel(tablero));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult EliminarTablero(TableroViewModel tab){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            _tableroRepositorio.BorrarTablero(tab.Id);
            return RedirectToAction("BorrarTareasTablero", "Tarea", new {id=tab.Id});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    
    public IActionResult EliminarTableroUsuario (int id){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            var listaTab=_tableroRepositorio.ListarTablerosDeUsuario(id);
            List<int> borrar = new List<int>();
            foreach (var item in listaTab)
            {
                borrar.Add(item.Id);
                _tableroRepositorio.BorrarTablero(item.Id);
            }
            return RedirectToAction("BorrarTareasTableros", "Tarea", new {lista = borrar});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("ListarUsuarios", "Usuario");
        }
    }
    
    public IActionResult Error(){
        return View(new ErrorViewModel());
    }

    public IActionResult ErrorLista(){
        return View();
    }
}

