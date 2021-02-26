using System.Collections.Generic;
using System.Linq;
using Todo.DataService.DataServices;
using Todo.Domain;

namespace Todo.DataService
{
    public class TodoListDataService : IDataService<TodoList>
    {
        readonly TodoContext _todoContext;

        public TodoListDataService(TodoContext context)
        {
            _todoContext = context;
        }

        public IEnumerable<TodoList> GetAll()
        {
            return _todoContext.TodoList.ToList();
        }

        public TodoList Get(long id)
        {
            return _todoContext.TodoList
                  .FirstOrDefault(e => e.TodoListId == id);
        }
        public void Add(TodoList entity)
        {
            _todoContext.TodoList.Add(entity);
            _todoContext.SaveChanges();
        }
        public void Update(TodoList todoList, TodoList entity)
        {
            todoList.UserId = entity.UserId;
            _todoContext.SaveChanges();
        }

        public void Delete(TodoList todoList)
        {
            _todoContext.TodoList.Remove(todoList);
            _todoContext.SaveChanges();
        }
    }
}
