using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudUserAPI.Models;
using CrudUserAPI.Data;

namespace CrudUserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly APIContext _context;

        public UserController(APIContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            return usuario;
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUsuario(User usuario)
        {
            // Verificar si el correo ya está registrado
            if (await _context.Usuarios.AnyAsync(u => u.CorreoElectronico == usuario.CorreoElectronico))
            {
                return Conflict(new { message = "El correo electrónico ya está registrado." });
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, User usuarioActualizado)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            // Actualizar solo los campos necesarios
            usuarioExistente.Nombre = usuarioActualizado.Nombre;
            usuarioExistente.CorreoElectronico = usuarioActualizado.CorreoElectronico;
            usuarioExistente.Telefono = usuarioActualizado.Telefono;
            usuarioExistente.Activo = usuarioActualizado.Activo;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario actualizado correctamente." });
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new { message = "El usuario no existe." });
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario eliminado correctamente." });
        }
    }
}
