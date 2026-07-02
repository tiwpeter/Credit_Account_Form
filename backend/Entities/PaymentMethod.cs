using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string PayCode { get; set; } = null!;

    public string PayName { get; set; } = null!;

    public string? PayDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
