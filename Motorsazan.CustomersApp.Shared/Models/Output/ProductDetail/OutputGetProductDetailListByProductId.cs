namespace Motorsazan.CustomersApp.Shared.Models.Output.ProductDetail
{
    public class OutputGetProductDetailListByProductId
    {
        public long ProductionId { get; set; }

        public string EngineType { get; set; }

        public int NumberOfCylinder { get; set; }

        public string MaxPower { get; set; }

        public string MaxTorque { get; set; }

        public decimal ApproximateWeight { get; set; }

        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public string ImageName { get; set; }

        public string CatalogName { get; set; }
    }
}