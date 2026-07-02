using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class ExchRateType
{
    public int Id { get; set; }

    public string ErTypeCode { get; set; } = null!;

    public string ErTypeName { get; set; } = null!;

    public string? ErTypeDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
