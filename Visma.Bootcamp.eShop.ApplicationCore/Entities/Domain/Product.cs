using System;
using System.ComponentModel.DataAnnotations;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain
{
    // db entity
    internal class Product
    {
        [Key]
        internal int Id { get; set; }

        [Required]
        public Guid? ProductId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        // foreign key
        [Required]
        public int CatalogId { get; set; }
        public virtual Catalog Catalog { get; set; }
    }
}
