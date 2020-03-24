using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Entities;
using System.Text.RegularExpressions;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthUser model)
        {
            var user = await userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User user = await userService.Get(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> NewUser([FromBody] User newUser)
        {
            //does stuff (checks if email)
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match matchEmail = regex.Match(newUser.Username);
            if (!matchEmail.Success)
            {
                return Problem("userName should be an email", title: "ValidationProblem");
            }
            //checks if password has a minimum of eight characters, at least one letter and one number
            Regex regexPass = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            Match matchPass = regexPass.Match(newUser.Password);
            if (!matchPass.Success)
            {
                return BadRequest(new { message = "password should contain a minimum of eight characters, at least one letter and one number.", title = "ValidationProblem" });
            }

            User user = await userService.NewUser(newUser.Username, newUser.Password, newUser.FirstName, newUser.LastName);

            return Ok(user);
        }
    }
}