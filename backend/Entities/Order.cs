using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Order
{
    public int? Id { get; set; }

    public string? UserEmail { get; set; }

    public float? TotalPrice { get; set; }

    public string? ShippingAddress { get; set; }

    public string? Phone { get; set; }

    public int? PaymentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? PaymentMethod { get; set; }
}
