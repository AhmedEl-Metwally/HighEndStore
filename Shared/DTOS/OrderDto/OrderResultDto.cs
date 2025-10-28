﻿namespace Shared.DTOS.OrderDto
{
    public record OrderResultDto
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; } = string.Empty;
        public AddressDto ShippingAddress { get; init; } = default!;
        public ICollection<OrderItemDto> OrderItems { get; init; } = new List<OrderItemDto>();
        public string PaymentStatus { get; init; } = string.Empty;
        public string DeliveryMethod { get; init; } = string.Empty;
        public int? DeliveryMethodId { get; init; }
        public decimal SubTotal { get; init; }
        public decimal Total { get; init; }
        public DateTimeOffset OrderDate { get;  init; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; init; } = string.Empty;
    }
}
