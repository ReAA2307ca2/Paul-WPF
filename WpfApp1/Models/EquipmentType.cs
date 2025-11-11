using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class EquipmentType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    public override string ToString()
    {
        return $"{TypeName}";
    }
}
