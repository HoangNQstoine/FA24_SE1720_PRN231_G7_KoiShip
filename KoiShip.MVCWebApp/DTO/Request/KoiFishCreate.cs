namespace KoiShip.MVCWebApp.DTO.Request
{
    public class KoiFishCreate
    {
        public int? UserId { get; set; }

        public int? CategoryId { get; set; }

        public string Name { get; set; }

        public double? Weight { get; set; }

        public int? Age { get; set; }

        public string ColorPattern { get; set; }

        public double? Price { get; set; }

        public string Description { get; set; }

        public string UrlImg { get; set; }

        public bool? Status { get; set; }
    }
}
