using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Todo.DataService;
using Todo.DataService.DataServices;
using Todo.Domain;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IDataService<Todo.Domain.Todo> _dataService;
        private readonly IUserDataService<User> _userDataService;

        public TodoController(IDataService<Todo.Domain.Todo> dataService, IUserDataService<User> userDataService)
        {
            _dataService = dataService;
            _userDataService = userDataService;
        }
        // GET: api/Todo
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {

            IEnumerable<Todo.Domain.Todo> todos = _dataService.GetAll();
            return Ok(todos);
        }

        // GET: api/Todo/5
        [HttpGet("{id}", Name = "GetTodo")]
        [Authorize(Policy = "ValidAccessToken")]
        public IActionResult Get(long id)
        {

            Todo.Domain.Todo todo = _dataService.Get(id);
            if (todo == null)
            {
                return NotFound("Todo couldn't be found.");
            }

            var email = User.Identity.Name;

            var user = _userDataService.GetByEmail(email);

            if(user.UserRole == 1)
            {
                return Ok(todo);
            }
            else
            {
                if (_userDataService.CanUserViewTodo(user, todo))
                {
                    return Ok(todo);
                }
                else
                {
                    return Unauthorized();
                }
            }
            
        }
        // POST: api/Todo
        [HttpPost]
        public IActionResult Post([FromBody] Todo.Domain.Todo todo)
        {
            if (todo == null)
            {
                return BadRequest("Todo is null.");
            }
            _dataService.Add(todo);
            return CreatedAtRoute(
                  "Get",
                  new { Id = todo.TodoId },
                  todo);
        }
        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Todo.Domain.Todo todo)
        {
            if (todo == null)
            {
                return BadRequest("Todo is null.");
            }
            Todo.Domain.Todo todoToUpdate = _dataService.Get(id);
            if (todoToUpdate == null)
            {
                return NotFound("The Todo record couldn't be found.");
            }
            _dataService.Update(todoToUpdate, todo);
            return NoContent();
        }
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Todo.Domain.Todo todo = _dataService.Get(id);
            if (todo == null)
            {
                return NotFound("The Todo record couldn't be found.");
            }
            _dataService.Delete(todo);
            return NoContent();
        }
    }
}
