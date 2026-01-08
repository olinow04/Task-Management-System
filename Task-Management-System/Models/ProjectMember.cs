using System.ComponentModel.DataAnnotations;

namespace Task_Management_System.Models
{
    public class ProjectMember
    {
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public MemberRole Role { get; set; } = MemberRole.Member;

        [Display(Name = "Data do³¹czenia")]
        public DateTime JoinedDate { get; set; } = DateTime.Now;
    }

    public enum MemberRole
    {
        Member,
        Manager,
        Viewer
    }
}
