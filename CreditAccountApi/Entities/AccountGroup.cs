using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class AccountGroup
{
    public int Id { get; set; }

    public string AccGroupCode { get; set; } = null!;

    public string AccGroupName { get; set; } = null!;

    public string? AccGroupDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
