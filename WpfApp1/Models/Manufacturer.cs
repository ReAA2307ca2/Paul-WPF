using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Manufacturer
{
    public int ManufacturerId { get; set; }

    public string ManufacturerName { get; set; } = null!;

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    public override string ToString()
    {
        return $"{ManufacturerName}";
    }
}
