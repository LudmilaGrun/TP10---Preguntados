using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PrimerProyecto.Models;
using Newtonsoft.Json; 

namespace PrimerProyecto.Controllers;

public class HomeController : Controller
{
    private static Juego JuegoActual; 

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ConfigurarJuego()
    {
        JuegoActual = new Juego();
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
            JuegoActual = new Juego();
            JuegoActual.CargarPartida(Username, Categoria); 
            return RedirectToAction("Jugar");
        }
    }

    public IActionResult Jugar()
    {
        Pregunta pregunta = JuegoActual.ObtenerProximaPregunta();

        if (pregunta == null)
        {
            ViewBag.Username = JuegoActual.Username;
            ViewBag.Puntaje = JuegoActual.PuntajeActual;
            return View("Fin");
        }

        List<Respuesta> respuestas = JuegoActual.ObtenerProximasRespuestas(pregunta.IdPregunta);

        ViewBag.Username = JuegoActual.Username;
        ViewBag.Puntaje = JuegoActual.PuntajeActual;
        ViewBag.NroPregunta = JuegoActual.NroPreguntaActual;
        ViewBag.Pregunta = pregunta;
        ViewBag.Respuestas = respuestas;

        return View("Juego");
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta)
    {
        bool esCorrecta = JuegoActual.VerificarRespuesta(idRespuesta);
        ViewBag.Correcta = esCorrecta;
        ViewBag.Username = JuegoActual.Username;
        ViewBag.Puntaje = JuegoActual.PuntajeActual;

        return View("Respuesta");
    }
}
