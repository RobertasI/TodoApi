using System.Collections.Generic;
using System.Linq;
using Todo.DataService.DataServices;
using Todo.Domain;

namespace Todo.DataService
{
    public class UserDataService : IDataService<User>
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
    }
}
