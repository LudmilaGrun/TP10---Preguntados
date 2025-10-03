using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Proyecto.Models
{
    public static class BD{
    private static string _connectionString = @"A-PHZ2-CEO-21";

   
    public static List<Categoria> ObtenerCategorias(){
        List<Categoria> Categorias = new List<Categoria>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Categorias";
                Categorias = connection.Query<Categoria>(query).ToList();
            }

            return categorias;
    }

    public static List<Pregunta> ObtenerPreguntas(int Categoria){
        List<Pregunta> Preguntas = new List<Pregunta>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (Categoria == -1)
                {
                    string query = "SELECT * FROM Preguntas";
                    Preguntas = connection.Query<Pregunta>(query).ToList();
                }
                else
                {
                    string query = "SELECT * FROM Preguntas WHERE IdCategoria = @pCategoria";
                    Preguntas = connection.Query<Pregunta>(query, new { pCategoria = Categoria }).ToList();
                }
            }

            return Preguntas;
    }

    public static List<Respuesta> ObtenerRespuestas(int idPregunta){
        List<Respuesta> Respuestas = new List<Respuesta>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Respuestas WHERE IdPregunta = @pidPregunta";
                Respuestas = connection.Query<Respuesta>(query, new { pidPregunta = idPregunta }).ToList();
            }

            return Respuestas;
    }
}
}
   
  