using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WpfApp1.Models;

public partial class Equipment
{
    public int EquipmentId { get; set; }

    public string Article { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string RentalUnit { get; set; } = null!;

    public decimal RentalCost { get; set; }

    public int SupplierId { get; set; }

    public int ManufacturerId { get; set; }

    public int TypeId { get; set; }

    public decimal? Discount { get; set; } = 0;

    public int AvailableQuantity { get; set; } = 0;

    public string? Description { get; set; }

    private string? _photo = "Default/picture.png";

    public string? Photo
    {
        get
        {
            return $"/Media/{this._photo}";
        }
        set
        {
            this._photo = value;
        }
    }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual EquipmentType Type { get; set; } = null!;
}
