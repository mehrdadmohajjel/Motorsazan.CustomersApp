using System.IO;

namespace Motorsazan.CustomersApp.Shared.Models.Input.ProductDetail
{
    public static class InputPdfViewer
    {
        public static long productionId { get; set; }

        public static FileStream  FilesArray { get; set; }
        public static MemoryStream  MemoryArray { get; set; }
    }
}
