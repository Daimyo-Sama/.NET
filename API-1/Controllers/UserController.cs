using API_1.Data;
using API_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //NECESSAIRES POUR ENTITY FRAMEWORK
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        // GetAll
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
           var response = await _context.Users.ToListAsync();
            return response;
        }

        // GetOne
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Aucun User Trouvé avec l'ID: " + id);
            }
            return Ok(user);

        }

        // Post
        [HttpPost]
        public async Task<ActionResult<List<User>>> CreateUser(User newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            var response = await _context.Users.ToListAsync();
            return response;


        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, User user)
        {
            var userData = await _context.Users.FindAsync(id);

            if (userData == null)
            {
                return NotFound("Aucun Livre Correspondant trouvé pour l'ID: " + id);
            }

            userData.UserId = user.UserId;
            userData.Username = user.Username;
            userData.Email = user.Email;
            userData.Password = user.Password;

            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        // DeleteOne
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var livre = await _context.Users.FindAsync(id);
            if (livre == null)
            {
                return NotFound("Aucun Livre Trouvé avec l'ID: " + id);
            }

            _context.Users.Remove(livre);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }
    }

        
}
