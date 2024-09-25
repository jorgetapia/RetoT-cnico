using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAccesoDatosReto.dbRetoTecnico;

namespace ApiAccesoDatosReto.Controllers
{
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
        public async Task<ActionResult<IEnumerable<VUsuario>>> GetUsuarios()
        {
            return await _context.VUsuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VUsuario>> GetUsuario(int id)
        {
            var usuario = await _context.VUsuarios.Where(c=>c.Idusuario==id).FirstAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }
       
        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (id != usuario.Idusuario)
                {
                    return BadRequest();
                }

                _context.Entry(usuario).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Fechacreacion = DateTime.Now;
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUsuario", new { id = usuario.Idusuario }, usuario);
            }
            else {
                return BadRequest(ModelState);//En caso de que no se cumplan las validaciones de entrada las regresamos al front para que se muestren al usuario.
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Idusuario == id);
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            // validamos el usuario, este debe estar activo
            if (_context.VUsuarios.Where(e => e.Usuario == request.usuario && e.Password==request.Password && e.Idestatus==1).Count()>0)
            {
                // Devolver éxito
                return Ok(new { token = "fake-jwt-token", success = true });
            }

            return Unauthorized(new { message = "Nombre de usuario o contraseña incorrectos, por favor verifique." });
        }

        public class LoginRequest
        {
            public string usuario { get; set; }
            public string Password { get; set; }
        }
    }
}
