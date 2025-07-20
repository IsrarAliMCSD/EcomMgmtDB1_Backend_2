using System;
using System.Collections.Generic;

namespace Code_EcomMgmtDB1.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Detail { get; set; }

    public bool? IsActive { get; set; }

    public byte[] ContentData { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public DateTime CreatdDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public byte[]? Image { get; set; }

    public string? ImageFormat { get; set; }

    public string? ImageName { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public decimal? ProductPrice { get; set; }

    public string? ProductUrl { get; set; }

    public int? SubCategoryId { get; set; }

    public virtual SubCategory? SubCategory { get; set; }
}
