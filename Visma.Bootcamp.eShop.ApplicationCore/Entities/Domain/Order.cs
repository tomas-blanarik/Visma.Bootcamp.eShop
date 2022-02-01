using System.ComponentModel.DataAnnotations;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain
{
    // db entity
    public class Order
    {
        [Key]
        public int Id { get; set; }
    }
}
