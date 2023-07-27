using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace secretaria_academica.Models
{
    public class ModuloModels 
    {
        public int IdModulo { get; set; }
        public string NomeModulo { get; set; }
        public int? CHModulo { get; set; }

        public CursoModels Curso { get; set; }

        public static List<ModuloModels> GetModulosByCurso(int cursoId)
        {
            List<ModuloModels> modulos = new List<ModuloModels>();

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT * FROM \"ModulosCursos\" WHERE \"CursoId\" = @CursoId";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@CursoId", cursoId);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ModuloModels modulo = new ModuloModels
                                {
                                    IdModulo = Convert.ToInt32(reader["Id"]),
                                    NomeModulo = reader["Nome"].ToString(),
                                    CHModulo = Convert.ToInt32(reader["CH"])
                                };
                                modulos.Add(modulo);
                            }
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                }
                catch (Exception ex)
                {
                }
            }

            return modulos;
        }
    }
}