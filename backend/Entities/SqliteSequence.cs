using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class SqliteSequence
{
    public string? Name { get; set; }

    public int? Seq { get; set; }
}
