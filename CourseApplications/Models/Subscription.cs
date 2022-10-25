using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseApplications.Models
{
  public class Subscription
  {
    [Key]
    public Guid SubscriptionId { get; set; }

    // Chaves estrangeiras
    [ForeignKey("CandidateId")]
    public Guid? CandidateId { get; set; }

    [ForeignKey("CourseId")]
    public Guid? CourseId { get; set; }


    // Propriedades de navegação
    public Candidate? Candidate { get; set; }
    public Course? Course { get; set; }

  }
}