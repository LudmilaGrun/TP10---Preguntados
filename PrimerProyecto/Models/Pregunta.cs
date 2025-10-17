using System.Collections.Generic;

namespace PrimerProyecto.Models;
public class Pregunta{
    public int IdPregunta { get; set; }
    public int IdCategoria { get; set; }
    public string Enunciado { get; set; } 
    public string Foto { get; set; }

    public Pregunta()
    {

    }
    
    public Pregunta( int pIdCategoria, string pEnunciado, string pFoto)
    {
        IdCategoria = IdCategoria;
        Enunciado = pEnunciado;
        Foto = pFoto;
    }
}