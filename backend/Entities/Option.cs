using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Option
{
    public int? Id { get; set; }

    public string? OptionType { get; set; }

    public string? OptionName { get; set; }

    public float? OptionPrice { get; set; }

    public string? ImageUrl { get; set; }

    public string? OptionTypeName { get; set; }
}
