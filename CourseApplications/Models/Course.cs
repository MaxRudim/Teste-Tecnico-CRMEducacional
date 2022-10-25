using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseApplications.Models
{
  public class Course
  {
    [Key]
    public Guid CourseId { get; set; }

    [Required]
    public string CourseName { get; set; }

    [ForeignKey("SubscriptionId")]
    public Guid SubscriptionId { get; set; }

    public Subscription? Subscription { get; set; }
  }
}