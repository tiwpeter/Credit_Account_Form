using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class CustomerSign
{
    public int CustsignId { get; set; }

    public string CustsignFirstname { get; set; } = null!;

    public string CustsignLastname { get; set; } = null!;

    public string? CustsignTel { get; set; }

    public string? CustsignEmail { get; set; }

    public string? CustsignLine { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
