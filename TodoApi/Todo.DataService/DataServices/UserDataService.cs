using System.Collections.Generic;
using System.Linq;
using Todo.DataService.DataServices;
using Todo.Domain;

namespace Todo.DataService
{
    public interface IUserDataService<T>
    {
        T GetByEmail(string email);

        bool IsPasswordValid(User user, string password);

        int GetRole(User user);

        bool CanUserManageTodo(User user, Todo.Domain.Todo todo);

        long GetUsersTodoListId(User user);

    }


    public class UserDataService : IDataService<User>, IUserDataService<User>
    {
        readonly TodoContext _todoContext;

        public UserDataService(TodoContext context)
        {
            _todoContext = context;
        }
        public IEnumerable<User> GetAll()
        {
            return _todoContext.User.ToList();
        }

        public User Get(long id)
        {
            return _todoContext.User
                  .FirstOrDefault(e => e.UserId == id);
        }

        public void Add(User entity)
        {
            _todoContext.User.Add(entity);
            _todoContext.SaveChanges();
        }
        public void Update(User user, User entity)
        {
            user.Email = entity.Email;
            user.Password = entity.Password;
            user.UserRole = entity.UserRole;
            _todoContext.SaveChanges();
        }
        
        public void Delete(User user)
        {
            _todoContext.User.Remove(user);
            _todoContext.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            return _todoContext.User
                  .FirstOrDefault(e => e.Email == email);
        }

        public bool IsPasswordValid(User user, string password)
        {
            return user.Password == password;
        }

        public int GetRole(User user)
        {
            return user.UserRole;
        }

        public bool CanUserManageTodo(User user, Todo.Domain.Todo todo)
        {
            var userList = _todoContext.TodoList.FirstOrDefault(u => u.UserId == user.UserId);

            if (userList != null)
            {
                return userList.TodoListId == todo.TodoListId;
            }
            else
            {
                return false;
            }

        }

        public long GetUsersTodoListId(User user)
        {
            return _todoContext.TodoList.FirstOrDefault(u => u.UserId == user.UserId).TodoListId;
        }
    }
}
