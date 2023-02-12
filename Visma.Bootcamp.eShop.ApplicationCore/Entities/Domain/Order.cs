using System.Linq;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain
{
    // db entity
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid? PublicId { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; }
        public string UserId { get; set; } = "Tomas";
        public virtual ICollection<OrderItem> Items { get; set; }

        public OrderDto ToDto(bool includeItems = false)
        {
            return new OrderDto
            {
                PublicId = this.PublicId,
                CreatedDate = this.CreatedDate,
                Items = includeItems
                    ? this.Items.Select(i => i.ToDto()).ToList()
                    : null,
                Amount = includeItems ? CalculateAmount() : 0
            };
        }

        private decimal CalculateAmount()
        {
            if (this.Items == null || this.Items.Count == 0)
            {
                return 0;
            }

            return this.Items.Sum(i => i.Price * i.Quantity);
        }
    }
}
