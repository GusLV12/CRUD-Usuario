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
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            return user;
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (await _context.Users.AnyAsync(u => u.CorreoElectronico == user.CorreoElectronico))
            {
                return Conflict(new { message = "El correo electrónico ya está registrado." });
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/User/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            existingUser.Nombre = updatedUser.Nombre;
            existingUser.CorreoElectronico = updatedUser.CorreoElectronico;
            existingUser.Telefono = updatedUser.Telefono;
            existingUser.Activo = updatedUser.Activo;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario actualizado correctamente." });
        }

        // DELETE: api/User/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "El usuario no existe." });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario eliminado correctamente." });
        }
    }
}
