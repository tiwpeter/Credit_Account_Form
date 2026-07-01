using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Incoterm
{
    public int Id { get; set; }

    public string IncotermCode { get; set; } = null!;

    public string IncotermName { get; set; } = null!;

    public string? IncotermDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
