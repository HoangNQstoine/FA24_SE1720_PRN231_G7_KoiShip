namespace KoiShip.MVCWebApp.DTO.Request
{
    public class ShippingOrderRequest
    {

        public int? UserId { get; set; }

        public int? PricingId { get; set; }

        public int? ShipMentId { get; set; }
        public string? AdressTo { get; set; }

        public string? PhoneNumber { get; set; }

        public int? TotalPrice { get; set; }

        public string? Description { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? ShippingDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public int? Status { get; set; }
    }
}
