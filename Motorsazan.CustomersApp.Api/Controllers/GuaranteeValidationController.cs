using Motorsazan.CustomersApp.Api.Business;
using Motorsazan.CustomersApp.Api.Filters;
using Motorsazan.CustomersApp.Shared.Models.Input.GuaranteeValidation;
using Motorsazan.CustomersApp.Shared.Models.Output.GuaranteeValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Motorsazan.CustomersApp.Api.Controllers
{
    [RoutePrefix("GuaranteeValidation")]
    public class GuaranteeValidationController : ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///دریافت تاییدیه اصالت کالا توسط شناسه گارانتی و سریال محصول
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetProductValidationByProductSerialAndGuaranteeSerial")]
        [RequestModelNullValidation]
        [RequestModelValidation]
        public IHttpActionResult GetProductValidationByProductSerialAndGuaranteeSerial(InputGetProductValidationByProductSerialAndGuaranteeSerial input)
        {
            const string storedProcedureName = "[Sale].[prc_GetProductValidationByProductSerialAndGuaranteeSerial]";

            var errorMessage = _businessManager.
                       CallStoredProcedureAndReturnMessageIfExits<InputGetProductValidationByProductSerialAndGuaranteeSerial,
                        OutputGetProductValidationByProductSerialAndGuaranteeSerial>(
                        storedProcedureName, input);
            return !string.IsNullOrEmpty(errorMessage.errorMessage)
               ? (IHttpActionResult)BadRequest(errorMessage.errorMessage)
              : Ok(errorMessage.output);
        }
    }
}