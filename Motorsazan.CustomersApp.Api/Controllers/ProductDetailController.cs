using Motorsazan.CustomersApp.Api.Business;
using Motorsazan.CustomersApp.Shared.Models.Input.ProductDetail;
using Motorsazan.CustomersApp.Shared.Models.Output.ProductDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Motorsazan.CustomersApp.Api.Controllers
{
    [RoutePrefix("ProductDetail")]
    public class ProductDetailController : ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        /// دریافت لسیت جزئیات محصولات بر اساس شناسه جدول محصولات 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("GetProductDetailListByProductId")]
        [HttpPost]
        public IHttpActionResult GetProductDetailListByProductId(InputGetProductDetailListByProductId input
            )
        {
            const string storedProcedureName = "[Sale].[prc_GetProductDetailListByProductId]";

            var result =
                _businessManager
                    .CallStoredProcedure<
                        InputGetProductDetailListByProductId,OutputGetProductDetailListByProductId[]>(
                        storedProcedureName,input);

            return Ok(result);
        }
    }
}
