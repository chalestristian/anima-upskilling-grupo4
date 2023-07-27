using inscricao_upskilling.Models;
using Npgsql;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace inscricao_upskilling.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(PessoaModel pessoa)
        {
            string Nome = pessoa.Nome;
            string Cpf = pessoa.Cpf;
            string Celular = pessoa.Celular;
            string Email = pessoa.Email;
            string Login = pessoa.Login;
            string Senha = pessoa.Senha;

            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            int PessoaId;
            int AlunoId;


            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string comando = $"INSERT INTO public.\"Pessoas\"(\"Nome\", \"CPF\", \"Celular\", \"Email\")VALUES('{Nome}', '{Cpf}', '{Celular}', '{Email}') RETURNING \"Id\"";
                using (NpgsqlCommand cmd = new NpgsqlCommand(comando, con))
                {
                    cmd.CommandText = comando;
                    cmd.Parameters.AddWithValue(comando);
                    PessoaId = (int)cmd.ExecuteScalar();
                }

                comando = $"INSERT INTO public.\"Alunos\"(\"PessoaId\", \"Matricula\", \"DataCadastro\")VALUES('{PessoaId}', '{PessoaId}2023{PessoaId}', 'NOW()') RETURNING \"Id\"";
                using (NpgsqlCommand cmd = new NpgsqlCommand(comando, con))
                {
                    cmd.CommandText = comando;
                    cmd.Parameters.AddWithValue(comando);
                    AlunoId = (int)cmd.ExecuteScalar();
                }

                comando = $"INSERT INTO public.\"Usuarios\"(\"PessoaId\", \"Login\", \"Senha\", \"DataUltimoAcesso\", \"DataCadastro\", \"AlunoId\", \"PerfilAdministrativo\", \"PerfilAluno\") VALUES('{PessoaId}', '{Login}', '{Senha}', 'NOW()', 'NOW()', {AlunoId}, false, true)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(comando, con))
                {
                    cmd.Parameters.AddWithValue(comando);
                    cmd.ExecuteNonQuery();
                }
                con.Close();

                return View();

            }
        }      


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }


}