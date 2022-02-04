using System;
using System.ComponentModel.DataAnnotations;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Examples
{
    public abstract class DomainBase
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
    }
}
