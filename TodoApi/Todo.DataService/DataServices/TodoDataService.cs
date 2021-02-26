using System.Collections.Generic;
using System.Linq;
using Todo.DataService.DataServices;

namespace Todo.DataService
{
    public class TodoDataService : IDataService<Domain.Todo>
    {
        readonly TodoContext _todoContext;

        public TodoDataService(TodoContext context)
        {
            _todoContext = context;
        }
        public IEnumerable<Domain.Todo> GetAll()
        {
            return _todoContext.Todo.ToList();
        }

        public Domain.Todo Get(long id)
        {
            return _todoContext.Todo
                  .FirstOrDefault(e => e.TodoId == id);
        }
        public void Add(Domain.Todo entity)
        {
            _todoContext.Todo.Add(entity);
            _todoContext.SaveChanges();
        }
        public void Update(Domain.Todo todo, Domain.Todo entity)
        {
            todo.Text = entity.Text;
            todo.IsDone = entity.IsDone;
            _todoContext.SaveChanges();
        }

        public void Delete(Domain.Todo todo)
        {
            _todoContext.Todo.Remove(todo);
            _todoContext.SaveChanges();
        }
    }
}
