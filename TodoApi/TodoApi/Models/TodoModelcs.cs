using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoPostModel
    {
        [Required(ErrorMessage = "Todo Text is required")]
        public string Text { get; set; }
    }

    public class TodoUpdateModel
    {
        public string Text { get; set; }

        public bool IsDone { get; set; }
    }

}
