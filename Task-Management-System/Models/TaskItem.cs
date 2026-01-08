using System.ComponentModel.DataAnnotations;

namespace Task_Management_System.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytu³ zadania jest wymagany")]
        [StringLength(200, ErrorMessage = "Tytu³ nie mo¿e byæ d³u¿szy ni¿ 200 znaków")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Opis nie mo¿e byæ d³u¿szy ni¿ 1000 znaków")]
        public string? Description { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Termin wykonania")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Priorytet jest wymagany")]
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        [Required(ErrorMessage = "Status jest wymagany")]
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;

        [Required(ErrorMessage = "Projekt jest wymagany")]
        [Display(Name = "Projekt")]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        [Display(Name = "Przypisane do")]
        public string? AssignedToUserId { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High,
        Critical
    }

    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Testing,
        Done,
        Blocked
    }
}
