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
        Juego juego = new Juego();
        juego.CargarPartida(Username, Categoria);

        HttpContext.Session.SetString("Jueg", Objeto.ObjectToString(juego));

        return RedirectToAction("Jugar");
    }

    [HttpPost]
    public IActionResult Jugar()
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("Jueg"));

        if (juego.ListaPreguntas.Count == 0)
        {
            HttpContext.Session.SetString("Jueg", Objeto.ObjectToString(juego));
            ViewBag.Username = juego.Username;
            ViewBag.Puntaje = juego.PuntajeActual;
            return View("Fin");
        }
        else
        {
            Pregunta preguntaActual = juego.ObtenerProximaPregunta();
            ViewBag.PreguntaActual = preguntaActual;

            ViewBag.ListaRespuestas = juego.ObtenerProximasRespuestas(preguntaActual.IdPregunta);

            HttpContext.Session.SetString("Jueg", Objeto.ObjectToString(juego));

            ViewBag.Username = juego.Username;
            ViewBag.Puntaje = juego.PuntajeActual;

            return View("Juego");
        }
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int idRespuesta)
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("Jueg"));

        ViewBag.Correcta = juego.VerificarRespuesta(idRespuesta);

        Pregunta preguntaActual = juego.ObtenerProximaPregunta();
        ViewBag.PreguntaActual = preguntaActual;

        if (preguntaActual != null)
        ViewBag.ListaRespuestas = juego.ObtenerProximasRespuestas(preguntaActual.IdPregunta);

        HttpContext.Session.SetString("Jueg", Objeto.ObjectToString(juego));

        ViewBag.Username = juego.Username;
        ViewBag.Puntaje = juego.PuntajeActual;

        return View("Juego");
    }
}
