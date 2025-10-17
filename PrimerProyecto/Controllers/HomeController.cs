using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PrimerProyecto.Models;

namespace PrimerProyecto.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ConfigurarJuego()
    {
        ViewBag.Categorias = BD.ObtenerCategorias();
        return View("ConfigurarJuego");
    }

    [HttpPost]
    public IActionResult Comenzar(string Username, int Categoria)
    {
        if (string.IsNullOrEmpty(Username))
        {
             return RedirectToAction("ConfigurarJuego");
        }

        Juego juego = new Juego();
        juego.CargarPartida(Username, Categoria);

        if (juego.ListaPreguntas.Count == 0)
        {
            return View("Fin");
        }

        HttpContext.Session.SetString("Juego", Objeto.ObjectToString(juego));
        return RedirectToAction("Jugar");
    }

    [HttpGet]
    public IActionResult Jugar()
    {
        string juego1 = HttpContext.Session.GetString("Juego");
        if (string.IsNullOrEmpty(juego1))
            return RedirectToAction("ConfigurarJuego");

        Juego juego = Objeto.StringToObject<Juego>(juego1);

        if (juego.PreguntaActual == null) 
        {
            return View("Fin");
        }

        ViewBag.PreguntaActual = juego.PreguntaActual;
        ViewBag.ListaRespuestas = juego.RespuestasActual;
        ViewBag.Username = juego.Username;
        ViewBag.Puntaje = juego.PuntajeActual;

        return View("Juego");
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int idRespuesta)
    {
        string juego2 = HttpContext.Session.GetString("Juego");
        if (string.IsNullOrEmpty(juego2))
            return RedirectToAction("ConfigurarJuego");

        Juego juego = Objeto.StringToObject<Juego>(juego2);

        juego.VerificarRespuesta(idRespuesta);

        HttpContext.Session.SetString("Juego", Objeto.ObjectToString(juego));

        if (juego.PreguntaActual == null) 
            return View("Fin");

        ViewBag.PreguntaActual = juego.PreguntaActual;
        ViewBag.ListaRespuestas = juego.RespuestasActual;
        ViewBag.Username = juego.Username;
        ViewBag.Puntaje = juego.PuntajeActual;

        return View("Juego");
    }
}
