using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolSystemAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        public Context.SchoolContext Context { get; set; }
        public userController(Context.SchoolContext _context)
        {
                 Context = _context;
        }

        //get all users
        [HttpGet]
        public IActionResult GettAll()
        {
            List<User> users = Context.Users.ToList();
            if (users.Count == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(users);
            }
        }
        //getuser by uisng Id
        [HttpGet("{id}:int")]
        public IActionResult GettStudentById(int id)
        {
            User user = Context.Users.Find(id);
            if (user == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(user);
            }
        }
        //Add New user
        [HttpPost]
        public IActionResult signin(User user)
        {
            User found = Context.Users.FirstOrDefault(u => u.email == user.email);
            if (ModelState.IsValid)
            {
                if (found == null)
                {
                    try
                    {
                        Context.Add(user);
                        Context.SaveChanges();
                        return Ok(new { message = "Success" });
                    }
                    catch
                    {
                        return Ok(new { message = "An error occurred while adding the user" });
                    }
                }
                else
                {
                    return Ok(new { message = " The email is already in use" });
                }
            }
            return BadRequest(new { message = "Validation errors" });
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login model)
        {
            
            var user = Context.Users.FirstOrDefault(u => u.email == model.email && u.password == model.password);

            if (user == null)
            {
                // User not found or credentials are incorrect
                return Ok(new { message = "there is not account with this email" });
            }

            // Generate a token
            string token = GenerateToken(user);
            //create sucess message
            string message = "Success";

            // Return the token and user data
            var response = new
            {
                Token = token,
                User = user,
                Message = message
            };

            return Ok(response);
        }
        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GenerateSecretKey()); 

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, (user.first_name+" "+user.last_name)),
                    new Claim(ClaimTypes.Email, user.email),
                    
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        // Generate a strong and random secret key 
        private string GenerateSecretKey()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var keyBytes = new byte[32]; // Use a key size of 256 bits (32 bytes)
                rng.GetBytes(keyBytes);
                return Convert.ToBase64String(keyBytes);
            }
        }
    }
}
