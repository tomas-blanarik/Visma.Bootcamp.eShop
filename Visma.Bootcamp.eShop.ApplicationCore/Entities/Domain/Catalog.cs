using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain
{
    // db entity
    internal class Catalog
    {
        [Key]
        internal int Id { get; set; }

        [Required]
        public Guid? CatalogId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        // one-to-many
        public virtual ICollection<Product> Products { get; set; }
    }
}
