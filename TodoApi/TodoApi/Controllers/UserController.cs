using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.DataService;
using Todo.DataService.DataServices;
using Todo.Domain;
using TodoApi.EmailService;

namespace TodoApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataService<User> _dataService;
        private readonly IUserDataService<User> _userDataService;
        private readonly IMailer _mailer;
        public UserController(IDataService<User> dataService, IMailer mailer, IUserDataService<User> userDataService)
        {
            _dataService = dataService;
            _mailer = mailer;
            _userDataService = userDataService;
        }
        // GET: api/User
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            IEnumerable<User> users = _dataService.GetAll();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get(long id)
        {
            User user = _dataService.Get(id);
            if (user == null)
            {
                return NotFound("User couldn't be found.");
            }
            return Ok(user);
        }
        // POST: api/User
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }
            _dataService.Add(user);
            return CreatedAtRoute(
                  "Get",
                  new { Id = user.UserId },
                  user);
        }
        // PUT: api/User/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(long id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }
            User userToUpdate = _dataService.Get(id);
            if (userToUpdate == null)
            {
                return NotFound("The User record couldn't be found.");
            }
            _dataService.Update(userToUpdate, user);
            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(long id)
        {
            User user = _dataService.Get(id);
            if (user == null)
            {
                return NotFound("The User record couldn't be found.");
            }
            _dataService.Delete(user);
            return NoContent();
        }

        [HttpPost]
        [Authorize]
        [Route("User/PasswordChangeEmail")]
        public async Task<IActionResult> Post()
        {
            string email = User.Identity.Name;
            string subject = "Password Change";
            //not the best sotulion I guess
            string message = "https://localhost:44399/User/ChangePassword";
            await _mailer.SendEmailAsync(email, subject, message);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("User/ChangePassword")]
        public IActionResult ChangePassword(string password)
        {
            var userToUpdate = _userDataService.GetByEmail(User.Identity.Name);

            if (userToUpdate == null)
            {
                return BadRequest("User is null.");
            }

            var user = new User {Email = userToUpdate.Email, Password = password, TodoList = userToUpdate.TodoList, 
                UserId = userToUpdate.UserId, UserRole = userToUpdate.UserRole};

            _dataService.Update(userToUpdate, user);

            return NoContent();
        }

    }
}
