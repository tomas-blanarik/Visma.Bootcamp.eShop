using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain
{
    // db entity
    public class Catalog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid? PublicId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        // one-to-many
        public virtual ICollection<Product> Products { get; set; }

        public CatalogDto ToDto(bool includeProducts = false)
        {
            return new CatalogDto
            {
                PublicId = this.PublicId.Value,
                Name = this.Name,
                Description = this.Description,
                Products = includeProducts
                    ? this.Products.Select(x => x.ToDto()).ToList()
                    : null
            };
        }
    }
}
