using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Todo.DataService.DataServices;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IDataService<Todo.Domain.Todo> _dataService;
        public TodoController(IDataService<Todo.Domain.Todo> dataService)
        {
            _dataService = dataService;
        }
        // GET: api/Todo
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Todo.Domain.Todo> todos = _dataService.GetAll();
            return Ok(todos);
        }
        // GET: api/Todo/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            Todo.Domain.Todo todo = _dataService.Get(id);
            if (todo == null)
            {
                return NotFound("Todo couldn't be found.");
            }
            return Ok(todo);
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
