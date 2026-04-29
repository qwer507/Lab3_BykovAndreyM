using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class Registration
{
    public int RId { get; set; }

    public string RLogin { get; set; } = null!;

    public string RPassword { get; set; } = null!;

    public string RRepeatPassword { get; set; } = null!;

    public bool RResult { get; set; }

    public string? RErrorMessage { get; set; }
}
