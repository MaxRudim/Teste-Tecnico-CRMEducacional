using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseApplications.Models
{
  public class Candidate
  {
    [Key]
    public Guid CandidateId { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email")]
    [Required]
    public string Email { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Invalid password length")]
    public string Password { get; set; }

    [Required]
    public string Cpf { get; set; }
    public ICollection<Subscription>? Subscriptions { get; set; }
  }
}