using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class AccountCode
{
    public int Id { get; set; }

    public string AccCode { get; set; } = null!;

    public string AccName { get; set; } = null!;

    public string? AccDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
