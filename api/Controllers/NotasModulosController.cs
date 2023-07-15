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
    public class NotasModulosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotasModulosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/NotasModulos
        [HttpGet]
        public ActionResult<IEnumerable<NotaModulo>> GetNotasModulos()
        {
            var notasModulos = _context.Notas.Include(n => n.Modulo).ToList();
            return Ok(notasModulos);
        }

        // GET: api/NotasModulos/1
        [HttpGet("{id}")]
        public ActionResult<NotaModulo> GetNotaModulo(int id)
        {
            var notaModulo = _context.Notas.Include(n => n.Modulo).FirstOrDefault(n => n.Id == id);
            if (notaModulo == null)
            {
                return NotFound();
            }
            return Ok(notaModulo);
        }

        // POST: api/NotasModulos
        [HttpPost]
        public ActionResult<NotaModulo> CreateNotaModulo(NotaModulo notaModulo)
        {
            _context.Notas.Add(notaModulo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetNotaModulo), new { id = notaModulo.Id }, notaModulo);
        }

        // PUT: api/NotasModulos/1
        [HttpPut("{id}")]
        public IActionResult UpdateNotaModulo(int id, NotaModulo notaModulo)
        {
            if (id != notaModulo.Id)
            {
                return BadRequest();
            }

            _context.Entry(notaModulo).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/NotasModulos/1
        [HttpDelete("{id}")]
        public IActionResult DeleteNotaModulo(int id)
        {
            var notaModulo = _context.Notas.FirstOrDefault(n => n.Id == id);
            if (notaModulo == null)
            {
                return NotFound();
            }

            _context.Notas.Remove(notaModulo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
