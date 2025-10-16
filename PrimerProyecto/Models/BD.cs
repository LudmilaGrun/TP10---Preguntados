using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;


namespace PrimerProyecto.Models;

public class BD
{
    private static string _connectionString = @"A-PHZ2-CIDI-36";

    public static List<Categoria> ObtenerCategorias()
    {
        List<Categoria> ListaCategorias = new List<Categoria>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Categorias";
            ListaCategorias = connection.Query<Categoria>(query).ToList();
        }

        return ListaCategorias;
    }

    public static List<Pregunta> ObtenerPreguntas(int categoria)
    {
        List<Pregunta> ListaPreguntas = new List<Pregunta>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query;

            if (categoria == -1)
            {
                query = "SELECT * FROM Preguntas";
                ListaPreguntas = connection.Query<Pregunta>(query).ToList();
            }
            else
            {
                query = "SELECT * FROM Preguntas WHERE IdCategoria = @Categoria";
                ListaPreguntas = connection.Query<Pregunta>(query, new { Categoria = categoria }).ToList();
            }
        }

        return ListaPreguntas;
    }

    public static List<Respuesta> ObtenerRespuestas(int idPregunta)
    {
        List<Respuesta> ListaRespuestas = new List<Respuesta>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Respuestas WHERE IdPregunta = @idPregunta";
            ListaRespuestas = connection.Query<Respuesta>(query, new { idPregunta }).ToList();
        }

        return ListaRespuestas;
    }
}
