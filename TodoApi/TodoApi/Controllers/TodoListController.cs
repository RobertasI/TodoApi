using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Todo.DataService.DataServices;
using Todo.Domain;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly IDataService<TodoList> _dataService;
        public TodoListController(IDataService<TodoList> dataService)
        {
            _dataService = dataService;
        }
        // GET: api/TodoList
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<TodoList> todoLists = _dataService.GetAll();
            return Ok(todoLists);
        }
        // GET: api/TodoList/5
        [HttpGet("{id}", Name = "GetTodoList")]
        public IActionResult Get(long id)
        {
            TodoList todoList = _dataService.Get(id);
            if (todoList == null)
            {
                return NotFound("Todo List couldn't be found.");
            }
            return Ok(todoList);
        }
        // POST: api/TodoList
        [HttpPost]
        public IActionResult Post([FromBody] TodoList todoList)
        {
            if (todoList == null)
            {
                return BadRequest("Todo List is null.");
            }
            _dataService.Add(todoList);
            return CreatedAtRoute(
                  "Get",
                  new { Id = todoList.TodoListId },
                  todoList);
        }
        // PUT: api/TodoList/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] TodoList todoList)
        {
            if (todoList == null)
            {
                return BadRequest("Todo List is null.");
            }
            TodoList todoListToUpdate = _dataService.Get(id);
            if (todoListToUpdate == null)
            {
                return NotFound("The Todo List record couldn't be found.");
            }
            _dataService.Update(todoListToUpdate, todoList);
            return NoContent();
        }
        // DELETE: api/TodoList/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            TodoList todoList = _dataService.Get(id);
            if (todoList == null)
            {
                return NotFound("The Todo List record couldn't be found.");
            }
            _dataService.Delete(todoList);
            return NoContent();
        }
    }
}
