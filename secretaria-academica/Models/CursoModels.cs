using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace secretaria_academica.Models
{
    public class CursoModels
    {
        public int IdCurso { get; set; }
        public string NomeCurso { get; set; }
        public int? CargaHoraria { get; set; }
        public decimal? ValorCurso { get; set; }

        public static CursoModels getByID(int idCurso)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            CursoModels curso = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT * FROM \"Cursos\" WHERE \"Id\" = @IdCurso";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IdCurso", idCurso);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                curso = new CursoModels
                                {
                                    IdCurso = Convert.ToInt32(reader["Id"]),
                                    NomeCurso = reader["Nome"].ToString(),
                                    CargaHoraria = Convert.ToInt32(reader["CH"]),
                                    ValorCurso = Convert.ToDecimal(reader["Valor"])
                                };
                            }
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return curso;
        }
    }
}

