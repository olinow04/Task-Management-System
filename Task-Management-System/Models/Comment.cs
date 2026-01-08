using System.ComponentModel.DataAnnotations;

namespace Task_Management_System.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Treœæ komentarza jest wymagana")]
        [StringLength(500, ErrorMessage = "Komentarz nie mo¿e byæ d³u¿szy ni¿ 500 znaków")]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "Data utworzenia")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}
