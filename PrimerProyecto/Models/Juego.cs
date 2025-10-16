using System.Collections.Generic;

namespace PrimerProyecto.Models
{
    public class Juego
    {
        public string Username { get; private set; }
        public int PuntajeActual { get; private set; }
        public int CantidadPreguntasCorrectas { get; private set; }
        public int ContadorNroPreguntaActual { get; private set; }
        public Pregunta PreguntaActual { get; private set; }
        public List<Pregunta> ListaPreguntas { get; private set; }
        public List<Respuesta> ListaRespuestas { get; private set; }
        public List<Respuesta> RespuestasActual { get; private set; }

        public Juego()
        {
            InicializarJuego();
        }

        private void InicializarJuego()
        {
            Username = null;
            PuntajeActual = 0;
            CantidadPreguntasCorrectas = 0;
            ContadorNroPreguntaActual = 0;
            PreguntaActual = null;
            ListaPreguntas = new List<Pregunta>();
            ListaRespuestas = new List<Respuesta>();
            RespuestasActual = new List<Respuesta>();
        }

        public void CargarPartida(string username, int categoria)
        {
            InicializarJuego();
            Username = username;
            ListaPreguntas = BD.ObtenerPreguntas(categoria);
        }

        public List<Categoria> ObtenerCategorias()
        {
            return BD.ObtenerCategorias();
        }

        public Pregunta ObtenerProximaPregunta()
        {
            if (ListaPreguntas != null && ContadorNroPreguntaActual < ListaPreguntas.Count)
            {
                PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
                ContadorNroPreguntaActual++;
                return PreguntaActual;
            }
            return null;
        }

        public List<Respuesta> ObtenerProximasRespuestas(int idPregunta)
        {
            RespuestasActual = BD.ObtenerRespuestas(idPregunta);
            return RespuestasActual;
        }

        public bool VerificarRespuesta(int idRespuesta)
        {
            bool correcta = false;

            if (RespuestasActual != null)
            {
                foreach (var respuesta in RespuestasActual)
                {
                    if (respuesta.Correcta && respuesta.IdRespuesta == idRespuesta)
                    {
                        correcta = true;
                        PuntajeActual += 10;
                        CantidadPreguntasCorrectas++;
                        break;
                    }
                }
            }

            return correcta;
        }


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
