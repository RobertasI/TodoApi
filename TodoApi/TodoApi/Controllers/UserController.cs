using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Todo.DataService.DataServices;
using Todo.Domain;

namespace TodoApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataService<User> _dataService;
        public UserController(IDataService<User> dataService)
        {
            _dataService = dataService;
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
    }
}
