using System;
using System.Collections.Generic;

namespace Code_EcomMgmtDB1.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string? Password { get; set; }

    public DateTime? Dob { get; set; }

    public int? RoleId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatdDate { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual Role? Role { get; set; }
}
