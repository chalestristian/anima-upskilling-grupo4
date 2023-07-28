using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Data;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PessoasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Pessoas
        [HttpGet]
        public ActionResult<IEnumerable<Pessoa>> GetPessoas()
        {
            var pessoas = _context.Pessoas.ToList();
            return Ok(pessoas);
        }

        // GET: api/Pessoas/1
        [HttpGet("{id}")]
        public ActionResult<Pessoa> GetPessoa(int id)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }

        // GET: api/Pessoas/12345678909
        [HttpGet("cpf/{cpf}")]
        public ActionResult<Pessoa> GetPessoaByCPF(string cpf)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.CPF == cpf);
            if (pessoa == null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }

        // POST: api/Pessoas
        [HttpPost]
        public ActionResult<Pessoa> CreatePessoa(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.Id }, pessoa);
        }

        // PUT: api/Pessoas/1
        [HttpPut("{id}")]
        public IActionResult UpdatePessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoa).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Pessoas/1
        [HttpDelete("{id}")]
        public IActionResult DeletePessoa(int id)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoas.Remove(pessoa);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
