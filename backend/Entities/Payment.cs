using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Payment
{
    public int? Id { get; set; }

    public string? Method { get; set; }
}
