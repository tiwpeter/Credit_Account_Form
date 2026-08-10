using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class ProductOption
{
    public int? Id { get; set; }

    public int? ProductId { get; set; }

    public int? OptionId { get; set; }
}
