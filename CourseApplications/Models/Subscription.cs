using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseApplications.Models
{
  public class Subscription
  {
    [Key]
    public Guid SubscriptionId { get; set; }

    // Chaves estrangeiras
    public Guid? CandidateId { get; set; }
    public Guid? CourseId { get; set; }


    // Propriedades de navegação
    public Candidate? Candidate { get; set; }
    public Course? Course { get; set; }

  }
}