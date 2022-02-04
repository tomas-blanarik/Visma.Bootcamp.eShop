using System.ComponentModel.DataAnnotations;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Examples
{
    internal class SecurityNumber : DomainBase
    {
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
