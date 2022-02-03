using System;
using System.ComponentModel.DataAnnotations;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Models
{
    public class BasketItemModel
    {
        [Required]
        public Guid? ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
