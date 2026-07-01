using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class CustGroupType
{
    public int Id { get; set; }

    public string CustgroTypeCode { get; set; } = null!;

    public string CustgroTypeName { get; set; } = null!;

    public string? CustgroTypeDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
