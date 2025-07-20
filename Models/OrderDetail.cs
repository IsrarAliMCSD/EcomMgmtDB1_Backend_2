using System;
using System.Collections.Generic;

namespace Code_EcomMgmtDB1.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderMasterId { get; set; }

    public int? ProductId { get; set; }

    public decimal? PriceOfEach { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalProductPrice { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatdDate { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual OrderMaster? OrderMaster { get; set; }
}
