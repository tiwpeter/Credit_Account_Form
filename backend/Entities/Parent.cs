using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Parent
{
    public int? Id { get; set; }

    public string? ParentName { get; set; }

    public string? ParentImageUrl { get; set; }
}
