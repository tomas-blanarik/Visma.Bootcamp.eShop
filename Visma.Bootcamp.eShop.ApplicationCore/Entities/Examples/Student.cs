using System.ComponentModel.DataAnnotations;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Examples
{
    internal class Student : DomainBase
    {
        [Required]
        public int SecurityNumberId { get; set; }
        public SecurityNumber SecurityNumber { get; set; }

        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }
    }
}
