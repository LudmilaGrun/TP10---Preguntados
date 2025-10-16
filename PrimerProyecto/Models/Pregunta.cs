using System.Collections.Generic;

namespace PrimerProyecto.Models;
public class Pregunta{
    public int IdPregunta { get; set; }
    public int IdCategorias { get; set; }
    public string Enunciado { get; set; } 
    public string Foto { get; set; }
    
    public Pregunta( int pIdCategorias, string pEnunciado, string pFoto)
    {
        IdCategorias = IdCategorias;
        Enunciado = pEnunciado;
        Foto = pFoto;
    }
}