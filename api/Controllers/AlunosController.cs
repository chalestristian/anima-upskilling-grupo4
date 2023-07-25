using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Data;
using Microsoft.AspNetCore.Authorization;
using api.Models.DTO;

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Alunos
        [HttpGet]
        public ActionResult<IEnumerable<Aluno>> GetAlunos()
        {
            var alunos = _context.Alunos.Include(a => a.Pessoa).ToList();
            return Ok(alunos);
        }

        // GET: api/Alunos/1
        [HttpGet("{id:int}")]
        public ActionResult<Aluno> GetAluno(int id)
        {
            var aluno = _context.Alunos.Include(a => a.Pessoa).FirstOrDefault(a => a.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return Ok(aluno);
        }

        // GET: api/Alunos/12345678909
        [HttpGet("cpf/{cpf}")]
        public ActionResult<Aluno> GetAlunoByCPF(string cpf)
        {
            var aluno = _context.Alunos.Include(a => a.Pessoa).FirstOrDefault(a => a.Pessoa.CPF == cpf);
            if (aluno == null)
            {
                return NotFound();
            }
            return Ok(aluno);
        }

        // GET: api/Alunos/123456
        [HttpGet("matricula/{matricula}")]
        public ActionResult<Aluno> GetAlunoByMatricula(string matricula)
        {
            var aluno = _context.Alunos.Include(a => a.Pessoa).FirstOrDefault(a => a.Matricula == matricula);
            if (aluno == null)
            {
                return NotFound();
            }
            return Ok(aluno);
        }

        // POST: api/Alunos
        [HttpPost]
        public ActionResult<Aluno> CreateAluno(AlunoCreateModel alunoCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pessoaExistente = _context.Pessoas.FirstOrDefault(p => p.Id == alunoCreateModel.PessoaId);

            if (pessoaExistente == null)
            {
                return BadRequest("A pessoa com o ID especificado não foi encontrada.");
            }

            var matriculaExistente = _context.Alunos.FirstOrDefault(a => a.Pessoa.Id == alunoCreateModel.PessoaId);

            if (matriculaExistente != null)
            {
                return Conflict($"A pessoa já possui outra matrícula. Matrícula atual: {matriculaExistente.Matricula}");
            }

            if (_context.Alunos.Any(a => a.Matricula == alunoCreateModel.Matricula))
            {
                return Conflict("A matrícula especificada já está em uso.");
            }

            var aluno = new Aluno
            {
                Pessoa = pessoaExistente,
                Matricula = alunoCreateModel.Matricula,
                DataCadastro = DateTime.UtcNow
            };

            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            aluno = _context.Alunos.FirstOrDefault(a => a.Id == aluno.Id);

            return Ok(aluno);
        }

        // PUT: api/Alunos/1
        [HttpPut("{id}")]
        public IActionResult UpdateAluno(int id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return BadRequest();
            }

            _context.Entry(aluno).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Alunos/1
        [HttpDelete("{id}")]
        public IActionResult DeleteAluno(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
