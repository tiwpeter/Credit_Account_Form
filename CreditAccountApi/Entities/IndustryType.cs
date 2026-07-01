using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class IndustryType
{
    public int Id { get; set; }

    public string InduTypeCode { get; set; } = null!;

    public string InduTypeName { get; set; } = null!;

    public string? InduTypeDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
