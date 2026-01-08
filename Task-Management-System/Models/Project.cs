using System.ComponentModel.DataAnnotations;

namespace Task_Management_System.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa projektu jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa projektu nie mo¿e byæ d³u¿sza ni¿ 100 znaków")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Opis nie mo¿e byæ d³u¿szy ni¿ 500 znaków")]
        public string? Description { get; set; }

        [Display(Name = "Data rozpoczêcia")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "Data zakoñczenia")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Status jest wymagany")]
        public ProjectStatus Status { get; set; } = ProjectStatus.Active;

        public string? OwnerId { get; set; }

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    }

    public enum ProjectStatus
    {
        Active,
        Completed,
        OnHold,
        Cancelled
    }
}
