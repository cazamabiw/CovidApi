using System;
using System.Collections.Generic;

namespace CovidApi.Models;

public partial class User
{
    public int Userid { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? Role { get; set; }
}
