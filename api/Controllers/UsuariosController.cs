using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Data;
using Microsoft.AspNetCore.Authorization;
using api.Models.DTO;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

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
            var usuarios = _context.Usuarios.Include(u => u.Pessoa).Include(u => u.Aluno).ToList();
            foreach(Usuario usuario in usuarios)
            {
                usuario.Senha = "*************";
            }
            return Ok(usuarios);
        }

        // GET: api/Usuarios/1
        [HttpGet("{id:int}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = _context.Usuarios.Include(u => u.Pessoa).Include(u => u.Aluno).FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.Senha = "*************";
            return Ok(usuario);
        }

        // GET: api/Usuarios/login
        [HttpGet("login/{login}")]
        public ActionResult<Usuario> GetUsuarioByLogin(string login)
        {
            var usuario = _context.Usuarios.Include(u => u.Pessoa).Include(u => u.Aluno).FirstOrDefault(u => u.Login == login);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.Senha = "*************";
            return Ok(usuario);
        }

        // POST: api/Usuarios
        [HttpPost]
        public ActionResult<Usuario> CreateUsuario(UsuarioCreateModel usuarioCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pessoaExistente = _context.Pessoas.FirstOrDefault(p => p.Id == usuarioCreateModel.PessoaId);

            if (pessoaExistente == null)
            {
                return BadRequest("A pessoa com o ID especificado não foi encontrada.");
            }

            var alunoExistente = _context.Alunos.FirstOrDefault(a => a.Id == usuarioCreateModel.AlunoId);

            if (alunoExistente == null)
            {
                return BadRequest("O aluno com o ID especificado não foi encontrado.");
            }

            if (_context.Usuarios.Any(u => u.Login == usuarioCreateModel.Login))
            {
                return Conflict("O login especificado já está em uso.");
            }

            var usuario = new Usuario
            {
                Pessoa = pessoaExistente,
                Login = usuarioCreateModel.Login,
                Senha = HashSenha(usuarioCreateModel.Senha, GenerateSalt()),
                Aluno = alunoExistente,
                PerfilAluno = usuarioCreateModel.PerfilAluno,
                DataCadastro = DateTime.UtcNow 
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            usuario.Senha = "*************";

            return Ok(usuario);
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

        // POST: api/Alunos/Login
        [HttpPost("Login")]
        public ActionResult Login(LoginModel loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = _context.Usuarios
                .Include(u => u.Pessoa)
                .Include(u => u.Aluno)
                .FirstOrDefault(u => u.Login == loginRequest.Login);

            if (usuario == null || !VerificarSenha(loginRequest.Senha, usuario.Senha))
            {
                return Unauthorized("Nome de usuário ou senha incorretos.");
            }

            usuario.Senha = "*************";

            return Ok(usuario);
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private string HashSenha(string senha, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}{hashed}";
        }

        private bool VerificarSenha(string senha, string hashSalvo)
        {
            byte[] salt = Convert.FromBase64String(hashSalvo.Substring(0, 24));

            string senhaHash = HashSenha(senha, salt);
            return hashSalvo == senhaHash;
        }
    }
}
