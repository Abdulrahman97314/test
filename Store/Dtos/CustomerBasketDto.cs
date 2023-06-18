﻿using Store.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Store.APIs.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal ShippingCost { get; set; }
        public int? DeliveryMethodId { get; set; }

    }
}
