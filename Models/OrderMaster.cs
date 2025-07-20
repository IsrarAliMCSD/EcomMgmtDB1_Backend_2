using System;
using System.Collections.Generic;

namespace Code_EcomMgmtDB1.Models;

public partial class OrderMaster
{
    public int OrderMasterId { get; set; }

    public int? UserId { get; set; }

    public int? CategoryId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatdDate { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
