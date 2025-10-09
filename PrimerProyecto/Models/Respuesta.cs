public class Respuesta{
    public int IdRespuesta { get; set; }
    public int IdPregunta { get; set; }
    public int Opcion { get; set; }         
    public string Contenido { get; set; } 
    public bool Correcta { get; set; }
    public string Foto { get; set; }
    
    public Respuesta (int pIdPregunta, int pOpcion, string pContenido, bool pCorrecta, string pFoto)
    {
        IdPregunta = pIdPregunta;
        Opcion = pOpcion;
        Contenido = pContenido;
        Correcta = pCorrecta;
        Foto = pFoto;
    }
}