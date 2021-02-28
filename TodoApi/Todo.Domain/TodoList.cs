using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Domain
{
    public class TodoList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TodoListId { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }

        public List<Todo> Todos { get; set; }
    }
}
