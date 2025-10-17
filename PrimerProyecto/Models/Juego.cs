using System.Collections.Generic;

namespace PrimerProyecto.Models
{
    public class Juego
    {
        // Propiedades públicas
        public string Username { get; set; }
        public int PuntajeActual { get; set; }
        public int CantidadPreguntasCorrectas { get; set; }
        public int ContadorNroPreguntaActual { get; set; }
        public Pregunta PreguntaActual { get; set; }
        public List<Pregunta> ListaPreguntas { get; set; }
        public List<Respuesta> RespuestasActual { get; set; }

        // Constructor
        public Juego()
        {
            InicializarJuego();
        }

        // Inicializa todas las propiedades
        private void InicializarJuego()
        {
            Username = "";
            PuntajeActual = 0;
            CantidadPreguntasCorrectas = 0;
            ContadorNroPreguntaActual = 0;
            PreguntaActual = null;
            ListaPreguntas = new List<Pregunta>();
            RespuestasActual = new List<Respuesta>();
        }

        // Carga las preguntas según la categoría y asigna la primera pregunta
        public void CargarPartida(string username, int categoria)
        {
            InicializarJuego();
            Username = username;

            if (categoria == -1)
                ListaPreguntas = BD.ObtenerPreguntasTodas();
            else
                ListaPreguntas = BD.ObtenerPreguntas(categoria);

            if (ListaPreguntas == null || ListaPreguntas.Count == 0)
            {
                ListaPreguntas = new List<Pregunta>();
                PreguntaActual = null;
                RespuestasActual = new List<Respuesta>();
                return;
            }

            // Inicializar la primera pregunta
            ContadorNroPreguntaActual = 0;
            PreguntaActual = ListaPreguntas[0];
            RespuestasActual = BD.ObtenerRespuestas(PreguntaActual.IdPregunta);
        }

        // Devuelve las categorías disponibles
        public List<Categoria> ObtenerCategorias()
        {
            return BD.ObtenerCategorias();
        }

        // Avanza a la siguiente pregunta si existe
        public Pregunta ObtenerProximaPregunta()
        {
            if (ContadorNroPreguntaActual + 1 < ListaPreguntas.Count)
            {
                ContadorNroPreguntaActual++;
                PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
                RespuestasActual = BD.ObtenerRespuestas(PreguntaActual.IdPregunta);
                return PreguntaActual;
            }

            // No hay más preguntas
            PreguntaActual = null;
            RespuestasActual = new List<Respuesta>();
            return null;
        }

        // Verifica si la respuesta elegida es correcta
        public bool VerificarRespuesta(int idRespuesta)
        {
            bool correcta = false;

            if (RespuestasActual != null)
            {
                foreach (var r in RespuestasActual)
                {
                    if (r.Correcta && r.IdRespuesta == idRespuesta)
                    {
                        correcta = true;
                        PuntajeActual += 10;
                        CantidadPreguntasCorrectas++;
                        break;
                    }
                }
            }

            // Solo avanzamos si hay más preguntas
            if (ContadorNroPreguntaActual + 1 < ListaPreguntas.Count)
            {
                ObtenerProximaPregunta();
            }
            else
            {
                PreguntaActual = null;
                RespuestasActual = new List<Respuesta>();
            }

            return correcta;
        }

        // Devuelve el ID de la pregunta más baja
        public int ObtenerIdPreguntaMasChico()
        {
            if (ListaPreguntas == null || ListaPreguntas.Count == 0)
                return -1;

            int idMinimo = ListaPreguntas[0].IdPregunta;
            foreach (var pregunta in ListaPreguntas)
            {
                if (pregunta.IdPregunta < idMinimo)
                    idMinimo = pregunta.IdPregunta;
            }

            return idMinimo;
        }
    }
}
