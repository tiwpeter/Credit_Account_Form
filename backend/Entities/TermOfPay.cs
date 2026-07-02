using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class TermOfPay
{
    public int Id { get; set; }

    public string TopCode { get; set; } = null!;

    public string TopName { get; set; } = null!;

    public string? TopDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
