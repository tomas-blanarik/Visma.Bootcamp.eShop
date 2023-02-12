﻿using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO
{
    public class OrderDto
    {
        public Guid? PublicId { get; set; }
        public DateTime? CreatedDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal Amount { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<OrderItemDto> Items { get; set; }
    }
}
