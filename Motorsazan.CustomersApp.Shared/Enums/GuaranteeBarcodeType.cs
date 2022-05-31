using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CustomersApp.Shared.Enums
{
    public enum GuaranteeBarcodeType
    {
        [Display(Name = "کد گارانتی")]
        GuaranteeSerial = 0,

        [Display(Name = "شماره سریال موتور")]
        ProductSerial = 1
    }
}
