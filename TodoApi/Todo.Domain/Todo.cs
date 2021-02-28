using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Domain
{
    public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TodoId { get; set; }

        public string Text { get; set; }

        public bool IsDone { get; set; }

        [ForeignKey("TodoList")]
        public long TodoListId { get; set; }
    }
}
