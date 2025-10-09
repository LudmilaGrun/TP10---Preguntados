public class Juego{
    private string Username;
    private int PuntajeActual;
    private int CantidadPreguntasCorrectas;
    private int ContadorNroPreguntaActual;
    private Pregunta PreguntaActual;
    private List<Pregunta> ListaPreguntas;
    private List<Respuesta> ListaRespuestas;

    public Juego()
    {
        InicializarJuego();
    }

    private void InicializarJuego()
    {
        Username = "";
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        ContadorNroPreguntaActual = 0;
        PreguntaActual = null;
        ListaPreguntas = null;
        ListaRespuestas = null;
    }

    public static List<Categoria> ObtenerCategorias()
    {
            return BD.ObtenerCategorias1();
    }
    public void CargarPartida(string UsernameElegido, int Categoria)
    {
        InicializarJuego(); 
        Username = UsernameElegido;
        ListaPreguntas = BD.ObtenerPreguntas(Categoria);
    }

    public Pregunta ObtenerProximaPregunta()
    {
        if (ListaPreguntas != null || ContadorNroPreguntaActual >= ListaPreguntas.Count)
        {
            PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
        }
        return PreguntaActual;
    }

    public List<Respuesta> ObtenerProximasRespuestas(int idPregunta)
    {
        ListaRespuestas = BD.ObtenerRespuestas(idPregunta);
        return ListaRespuestas;
    }

    public bool VerificarRespuesta(int idRespuesta)
    {
        bool EsCorrecta = false;

        if (listaRespuestas != null)
        {
    
            foreach (Respuesta respuesta in ListaRespuestas)
            {
                if (respuesta.IdRespuesta == idRespuesta)
                {
                    if (respuesta.Correcta)
                    {
                        EsCorrecta = true;
                        PuntajeActual += 10;  
                        CantidadPreguntasCorrectas++;
                    }
                }
            }

            ContadorNroPreguntaActual++;

            if (ContadorNroPreguntaActual < ListaPreguntas.Count)
            {
                PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
            }
            else
            {
                PreguntaActual = null; 
            }
        }   

            return EsCorrecta;
    }

}
    
