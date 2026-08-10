using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Discount
{
    public int? Id { get; set; }

    public int? ProductId { get; set; }

    public float? DiscountPercent { get; set; }

    public string? DiscountStartTime { get; set; }

    public string? DiscountEndTime { get; set; }
}
