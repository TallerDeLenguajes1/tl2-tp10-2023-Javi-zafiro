using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Javi_zafiro.Models;

namespace tl2_tp10_2023_Javi_zafiro.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new{controller= "Login", action="Index"});
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
