using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class DocumentCredit
{
    public int DoccreditId { get; set; }

    public bool? CompanyCertificate { get; set; }

    public bool? CopyOfPp20 { get; set; }

    public bool? CopyOfCoRegis { get; set; }

    public bool? CopyOfIdCard { get; set; }

    public bool? CompanyLocationMap { get; set; }

    public string? OtherSpecify { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
