using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Context.Models
{
    internal class Todo : IBaseModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public List<TodoItem> TodoItems { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
