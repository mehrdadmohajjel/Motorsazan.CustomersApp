using Motorsazan.CustomersApp.Shared.Attributes;

namespace Motorsazan.CustomersApp.Shared.Models.Input.NotificationSlider
{
    public class InputGetNotificationSliderAttachmentFileByFileName
    {
        [StoredProcedureParameter(Size = 200)] public string FileName { get; set; }
    }
}