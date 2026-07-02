using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class SortKey
{
    public int Id { get; set; }

    public string SortkeyCode { get; set; } = null!;

    public string SortkeyName { get; set; } = null!;

    public string? SortkeyDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
