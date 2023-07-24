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
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = _context.Usuarios.Include(u => u.Pessoa).ToList();
            return Ok(usuarios);
        }

        // GET: api/Usuarios/1
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = _context.Usuarios.Include(u => u.Pessoa).FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // GET: api/Usuarios/login
        [HttpGet("{login}")]
        public ActionResult<Usuario> GetUsuarioByLogin(string login)
        {
            var usuario = _context.Usuarios.Include(u => u.Pessoa).FirstOrDefault(u => u.Login == login);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // POST: api/Usuarios
        [HttpPost]
        public ActionResult<Usuario> CreateUsuario(Usuario usuario)
        {
            // Verificar se a pessoa já existe no banco de dados pelo CPF
            var pessoaExistente = _context.Pessoas.FirstOrDefault(p => p.CPF == usuario.Pessoa.CPF);

            if (pessoaExistente != null)
            {
                // Se a pessoa já existe, associa ela ao usuário
                usuario.Pessoa = pessoaExistente;
            }

            var alunoExistente = _context.Alunos.Include(a => a.Pessoa).FirstOrDefault(a => a.Pessoa.CPF == usuario.Pessoa.CPF);

            if (alunoExistente != null)
            {
                // Se o aluno já existe, associa ela ao usuário
                usuario.Aluno = alunoExistente;
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PUT: api/Usuarios/1
        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Usuarios/1
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
