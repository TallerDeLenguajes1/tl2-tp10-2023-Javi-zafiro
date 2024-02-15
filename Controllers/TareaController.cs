using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;
using repositorioParaKamba;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class TareaController : Controller
{
    private readonly ITareaRepository _tareaRepositorio;
    private readonly ITableroRepository _tableroRepositorio;
    private readonly IUsuarioRepository _usuarioRepositorio;
    private readonly ILogger<UsuarioController> _logger;

    public TareaController(ILogger<UsuarioController> logger, ITareaRepository tareaRepositorio,ITableroRepository tableroRepositorio, IUsuarioRepository usuarioRepositorio)
    {
        _logger=logger;
        _tareaRepositorio = tareaRepositorio;
        _usuarioRepositorio = usuarioRepositorio;
        _tableroRepositorio = tableroRepositorio;
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
            if (lista.Count<=0)
            {
                return RedirectToAction("ErrorListaUsuario");
            }else{
                if (HttpContext.Session.GetString("rol")==TiposUsuario.Administrador.ToString())
                {
                    return View(new ListarTareasViewModel(lista, true));
                }else{
                    return View(new ListarTareasViewModel(lista, false));
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("ErrorListaUsuario");
        }
        
    }

    [HttpGet]
    public IActionResult ListarTareasTablero(int id)
    {
        try
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
            var lista= _tareaRepositorio.ListarTareasPorTablero(id);
            if (lista.Count<=0)
            {
                return RedirectToAction("ErrorLista", new {idtab=id});
            }else
            {
                var tab= _tableroRepositorio.ObtenerTablero(id);
                var idPropietario=tab.IdUsuariPropietario;
                int idusu;
                var idSession=HttpContext.Session.GetString("id");
                int.TryParse(idSession, out idusu);
                if (HttpContext.Session.GetString("rol")==TiposUsuario.Administrador.ToString())
                {
                    return View(new ListarTareasViewModel(lista, true, idPropietario, idusu, tab.Nombre));
                }else{
                    return View(new ListarTareasViewModel(lista, false, idPropietario, idusu, tab.Nombre));
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("ErrorLista", new {idtab=id});
        }
    }

    [HttpGet]
    public IActionResult CrearTarea(int Id)
    {
        try
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
            return View(new CrearTareaViewModel(Id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel tar)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            if (ModelState.IsValid)
            {
                _tareaRepositorio.CrearTarea(new tarea(tar));
                return RedirectToRoute(new{controller= "Tablero", action="ListarTableros"});
            }
        return View(tar) ;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            var tarea = _tareaRepositorio.ObtenerTarea(id);
        if (tarea!=null)
        {
            var lista = _usuarioRepositorio.ListarUsuarios();
            return View(new TareaViewModel(tarea, lista));
        }
        return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult ModificarTarea(TareaViewModel tar){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            if (ModelState.IsValid)
            {
                _tareaRepositorio.ModificarTarea(tar.Id, new tarea(tar));
                return RedirectToAction("ListarTareasTablero", new {id = tar.IdTablero});
            }
        return View(tar);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult AsignarUsuario(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            var tarea = _tareaRepositorio.ObtenerTarea(id);
        if (tarea!=null)
        {
            var lista = _usuarioRepositorio.ListarUsuarios();
            return View(new AsignarUsuarioViewModel(tarea, lista));
        }
        return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult AsignarUsuario(AsignarUsuarioViewModel tar){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            _tareaRepositorio.AsignarTareaAUsuario(tar.Usuario_asignado, tar.Id );
            return RedirectToAction("ListarTareasTablero", new {id = tar.IdTablero});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult CambiarEstado(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            var tarea = _tareaRepositorio.ObtenerTarea(id);
        if (tarea!=null)
        {
            return View(new CambiarEstadoViewModel(tarea));
        }
        return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult CambiarEstado(CambiarEstadoViewModel tar){
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            _tareaRepositorio.CambiarEstado(tar.Id, tar.Estado);
            return RedirectToAction("ListarTareasTablero", new {id = tar.IdTablero});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult EliminarTarea(int id)
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
        try
        {
            var tarea = _tareaRepositorio.ObtenerTarea(id);
            if (tarea!=null)
            {
                return View(new TareaViewModel(tarea));
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult EliminarTarea(TareaViewModel tar){
        try
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
            _tareaRepositorio.BorrarTarea(tar.Id);
            return RedirectToAction("ListarTareas");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    public IActionResult BorrarTareasTableros(List<int> lista){
        try
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
            foreach (var item in lista)
            {
                var listaTar=_tareaRepositorio.ListarTareasPorTablero(item);
                if(listaTar.Count>0){
                    foreach (var t in listaTar)
                    {
                        _tareaRepositorio.BorrarTarea(t.Id);
                    }
                }

            }
            return RedirectToAction("ListarUsuarios", "Usuario");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    public IActionResult BorrarTareasTablero(int id){
        try
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="index"});
            var listaTar=_tareaRepositorio.ListarTareasPorTablero(id);
            if(listaTar.Count>0){
                foreach (var t in listaTar)
                {
                    _tareaRepositorio.BorrarTarea(t.Id);
                }
            }
            return RedirectToAction("ListarTableros", "Tablero");
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
    public IActionResult ErrorLista(int idtab){
        return View(idtab);
    }

    public IActionResult ErrorListaUsuario(){
        return View();
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