using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class OptionImage
{
    public int? Id { get; set; }

    public int? OptionId { get; set; }

    public string? ImageUrl { get; set; }
}
