using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Domain
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        public string Email { get; set; }

        [MinLength(12)]
        public string Password { get; set; }

        public int UserRole { get; set; }

        public List<TodoList> TodoList { get; set; }
    }
}
