using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PrimerProyecto.Models;
using Newtonsoft.Json; 

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
        if (JuegoActual == null)
        {
            Juego JuegoActual = new Juego();
        }
            else
        {
            Juego JuegoActual = new Juego();
        }

            ViewBag.Categorias = Juego.ObtenerCategorias();
            return View();
        
    }

    public IActionResult Comenzar(string Username, int Categoria)
    {
        if (Username == "" || Username == null)
        {
            return RedirectToAction("ConfigurarJuego");
        }
        else
        {
            Juego JuegoActual = new Juego();
            Juego.CargarPartida(Username, Categoria);
            return RedirectToAction("Jugar");
        }
    }

    public IActionResult Jugar()
    {
        Pregunta Pregunta = Juego.ObtenerProximaPregunta();

        if (Pregunta == null)
        {
            ViewBag.Username = Juego.Username;
            ViewBag.Puntaje = Juego.PuntajeActual;
            return View("Fin");
        }

        List<Respuesta> Respuestas = Juego.ObtenerProximasRespuestas(Pregunta.IdPregunta);

        ViewBag.Username = Juego.Username;
        ViewBag.Puntaje = Juego.PuntajeActual;
        ViewBag.NroPregunta = Juego.NroPreguntaActual;
        ViewBag.Pregunta = Pregunta;
        ViewBag.Respuestas = Respuestas;

        return View("Juego");
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta)
    {
        bool EsCorrecta = Juego.VerificarRespuesta(idRespuesta);
        ViewBag.Correcta = EsCorrecta;

        return View("Respuesta");
    }
}
