using System;
using System.Collections.Generic;
using System.Text;

namespace Motorsazan.CustomersApp.Shared.Models.Output.GuaranteeValidation
{
    public class OutputGetProductValidationByProductSerialAndGuaranteeSerial
    {
        public string GuaranteeSerial { get; set; }

        public string ProductName { get; set; }

        public string EngineTypeDescription { get; set; }

        public string EngineAppDescription { get; set; }

        public string ProductSerial { get; set; }

        public string CustomerName { get; set; }

        public string Result { get; set; }

    }
}
