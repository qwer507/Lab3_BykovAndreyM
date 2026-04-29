using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class Registration
{
    public int RId { get; set; }

    public string? RLogin { get; set; }

    public string? RPassword { get; set; }

    public string? RRepeatPassword { get; set; }

    public bool RResult { get; set; }

    public string? RErrorMessage { get; set; }
}
