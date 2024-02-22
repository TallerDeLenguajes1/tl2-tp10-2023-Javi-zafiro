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
    private readonly IUsuarioRepository _usuarioRepositorio;
    private readonly ILogger<UsuarioController> _logger;

    public TableroController(ILogger<UsuarioController> logger, ITableroRepository tableroRepositorio, ITareaRepository tareaRepositorio, IUsuarioRepository usuarioRepositorio)
    {
        _logger = logger;
        _tableroRepositorio = tableroRepositorio;
        _tareaRepositorio = tareaRepositorio;
        _usuarioRepositorio = usuarioRepositorio;
    }

    public IActionResult Index()
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        return View();
    }
    [HttpGet]
    public IActionResult ListarTablerosAdmin(){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        try
        {
            List<tablero> lista;
            List<tablero> listaPropios;
            int idusu;
            var id=HttpContext.Session.GetString("id");
            int.TryParse(id, out idusu);
            listaPropios = _tableroRepositorio.ListarTablerosDeUsuario(idusu);
            lista= _tableroRepositorio.ListarTableros();
            if (listaPropios.Count>0)
            {
                if(lista.Count>0){
                    lista.RemoveAll(item => listaPropios.Exists(t => t.Id == item.Id));
                }else{
                    _logger.LogWarning("Lista de Tableros no Propios Vacia");
                }
                
            }else{
                _logger.LogWarning("Lista de Tus Tableros Vacia");
            }
            var listaVM = new ListaTodosTablerosViewModel(listaPropios, lista);
            if (listaVM.ListaTabNoPropios.Count>0)
            {
                foreach (var item in listaVM.ListaTabNoPropios)
                {
                    var usu = _usuarioRepositorio.ObtenerUsuario(item.IdUsuariPropietario);
                    item.Usuario=usu.NombreDeUsuario;
                }
            }
            return View(listaVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("ErrorLista");
        }
    }
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
                    return RedirectToAction("ListarTablerosAdmin");
                }else
                {
                    List<tablero> listaPropios;
                    List<tablero> listaNoPropios;
                    List<TareaViewModel> list;
                    int idusu;
                    var id=HttpContext.Session.GetString("id");
                    int.TryParse(id, out idusu);
                    listaPropios = _tableroRepositorio.ListarTablerosDeUsuario(idusu);
                    list=_tareaRepositorio.ListarTareasPorUsuario(idusu);
                    if (list.Count>0)
                    {
                        listaNoPropios=_tableroRepositorio.ListarTablerosPorTareas(list);
                        listaNoPropios.RemoveAll(item => listaPropios.Exists(t => t.Id == item.Id));
                        var listas = new ListaTablerosViewModel(listaPropios, listaNoPropios);
                        if (listas.ListaTabPropios.Count>0)
                        {
                            foreach (var item in listas.ListaTabPropios)
                            {
                                var usu = _usuarioRepositorio.ObtenerUsuario(item.IdUsuariPropietario);
                                item.Usuario=usu.NombreDeUsuario;
                            }
                        }else{
                            _logger.LogWarning("Lista de Tableros Propios Vacia");
                        }
                        
                        foreach (var item in listas.ListaTabNoPropios)
                        {
                            var usu = _usuarioRepositorio.ObtenerUsuario(item.IdUsuariPropietario);
                            item.Usuario=usu.NombreDeUsuario;
                        }
                        return View(listas);
                    }else{
                        _logger.LogWarning("Lista de Tableros No Propios Vacia");
                        var listaVM = new ListaTablerosViewModel(listaPropios);
                        foreach (var item in listaVM.ListaTabPropios)
                        {
                            var usu = _usuarioRepositorio.ObtenerUsuario(item.IdUsuariPropietario);
                            item.Usuario=usu.NombreDeUsuario;
                        }
                        return View(listaVM);
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
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        return View(new ErrorViewModel());
    }

    public IActionResult ErrorLista(){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        return View();
    }
}

