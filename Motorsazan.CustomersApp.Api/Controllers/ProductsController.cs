using Motorsazan.CustomersApp.Api.Business;
using Motorsazan.CustomersApp.Shared.Models.Output.Products;
using System.Web.Http;

namespace Motorsazan.CustomersApp.Api.Controllers
{
    [RoutePrefix("Products")]
    public class ProductsController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        /// دریافت لیست گروه های محصولات 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("GetProductCategoryList")]
        [HttpPost]
        public IHttpActionResult GetProductCategoryList(
            )
        {
            const string storedProcedureName = "[Sale].[prc_GetProductCategoryList]";

            var result =
                _businessManager
                    .CallStoredProcedure<
                        OutputGetProductCategoryList[]>(
                        storedProcedureName);

            return Ok(result);
        }
    }
}
