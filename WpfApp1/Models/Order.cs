using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public decimal OrderNumber { get; set; }

    public int EquipmentId { get; set; }

    public int RentalQuantity { get; set; }

    public DateTime RentalStartDate { get; set; }

    public int PickupPointId { get; set; }

    public int ClientUserId { get; set; }

    public decimal ReceiptCode { get; set; }

    public int StatusId { get; set; }

    public virtual User ClientUser { get; set; } = null!;

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual PickupPoint PickupPoint { get; set; } = null!;

    public virtual OrderStatus Status { get; set; } = null!;
}
