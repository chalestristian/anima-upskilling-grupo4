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
    public class AplicacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AplicacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Aplicacoes
        [HttpGet]
        public ActionResult<IEnumerable<Aplicacao>> GetAplicacoes()
        {
            var aplicacoes = _context.Aplicacoes.ToList();
            return Ok(aplicacoes);
        }

        // GET: api/Aplicacoes/1
        [HttpGet("{id}")]
        public ActionResult<Aplicacao> GetAplicacao(int id)
        {
            var aplicacao = _context.Aplicacoes.FirstOrDefault(a => a.Id == id);
            if (aplicacao == null)
            {
                return NotFound();
            }
            return Ok(aplicacao);
        }

        // POST: api/Aplicacoes
        [HttpPost]
        public ActionResult<Aplicacao> CreateAplicacao(Aplicacao aplicacao)
        {
            _context.Aplicacoes.Add(aplicacao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAplicacao), new { id = aplicacao.Id }, aplicacao);
        }

        // PUT: api/Aplicacoes/1
        [HttpPut("{id}")]
        public IActionResult UpdateAplicacao(int id, Aplicacao aplicacao)
        {
            if (id != aplicacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(aplicacao).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Aplicacoes/1
        [HttpDelete("{id}")]
        public IActionResult DeleteAplicacao(int id)
        {
            var aplicacao = _context.Aplicacoes.FirstOrDefault(a => a.Id == id);
            if (aplicacao == null)
            {
                return NotFound();
            }

            _context.Aplicacoes.Remove(aplicacao);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
