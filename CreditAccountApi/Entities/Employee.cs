using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public string EmpCode { get; set; } = null!;

    public string EmpName { get; set; } = null!;

    public string? EmpDepartment { get; set; }
}
