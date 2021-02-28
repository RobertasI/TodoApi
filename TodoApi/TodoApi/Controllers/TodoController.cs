using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Todo.DataService;
using Todo.DataService.DataServices;
using Todo.Domain;
using TodoApi.Models;

namespace TodoApi.Controllers
{
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

            var user = _userDataService.GetByEmail(User.Identity.Name);

            if(user.UserRole == 1)
            {
                return Ok(todo);
            }
            else
            {
                if (_userDataService.CanUserManageTodo(user, todo))
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
        [Authorize(Policy = "ValidAccessToken")]
        public IActionResult Post([FromBody] TodoPostModel todo)
        {
            var user = _userDataService.GetByEmail(User.Identity.Name);
            if (user.UserRole == 1)
            {
                return BadRequest("Administrator cannot create new todo");
            }

            if (todo == null)
            {
                return BadRequest("Todo is null.");
            }

            var newTodo = new Todo.Domain.Todo { Text = todo.Text, IsDone = false, TodoListId = _userDataService.GetUsersTodoListId(_userDataService.GetByEmail(User.Identity.Name)) };
            _dataService.Add(newTodo);
            return CreatedAtRoute(
                  "GetTodo",
                  new { Id = newTodo.TodoId },
                  todo);
        }
        // PUT: api/Todo/5
        [HttpPut("{id}")]
        [Authorize(Policy = "ValidAccessToken")]
        public IActionResult Put(long id, [FromBody] TodoUpdateModel todo)
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

            var user = _userDataService.GetByEmail(User.Identity.Name);

            if (_userDataService.CanUserManageTodo(user, todoToUpdate))
            {
                _dataService.Update(todoToUpdate, new Todo.Domain.Todo { Text = todo.Text, IsDone = todo.IsDone });
                return NoContent();
            }
            else
            {
                return Unauthorized();
            }


        }
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ValidAccessToken")]
        public IActionResult Delete(long id)
        {
            Todo.Domain.Todo todo = _dataService.Get(id);
            if (todo == null)
            {
                return NotFound("The Todo record couldn't be found.");
            }

            var user = _userDataService.GetByEmail(User.Identity.Name);


            if (user.UserRole == 1)
            {
                _dataService.Delete(todo);
                return NoContent();
            }
            else
            {
                if (_userDataService.CanUserManageTodo(user, todo))
                {
                    _dataService.Delete(todo);
                    return NoContent();
                }
                else
                {
                    return Unauthorized();
                }
            }

        }
    }
}
