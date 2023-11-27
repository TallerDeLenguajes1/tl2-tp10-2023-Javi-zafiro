using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;
using repositorioParaKamba;
using modelosParaKamba;

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
        var usuarios = usuarioRepositorio.ListarUsuarios();
        return View(usuarios);
    }
    [HttpGet("usuario")]
    public IActionResult ListarUsuarios()
    {
        var lista= usuarioRepositorio.ListarUsuarios();
        return View(lista);
    }
}