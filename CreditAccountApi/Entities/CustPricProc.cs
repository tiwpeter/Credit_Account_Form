using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class CustPricProc
{
    public int Id { get; set; }

    public string CpProcCode { get; set; } = null!;

    public string CpProcName { get; set; } = null!;

    public string? CpProcDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
