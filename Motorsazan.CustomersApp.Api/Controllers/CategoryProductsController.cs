using Motorsazan.CustomersApp.Api.Business;
using Motorsazan.CustomersApp.Shared.Models.Input.CategoryProducts;
using Motorsazan.CustomersApp.Shared.Models.Output.CategoryProducts;
using System.Web.Http;

namespace Motorsazan.CustomersApp.Api.Controllers
{
    [RoutePrefix("CategoryProducts")]
    public class CategoryProductsController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///  دریافت لسیت محصولات بر اساس شناسه جدول گروه بندی محصولات
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("GetProductListByProductCategoryId")]
        [HttpPost]
        public IHttpActionResult GetProductListByProductCategoryId(InputGetProductListByProductCategoryId input)
        {
            const string storedProcedureName = "[Sale].[prc_GetProductListByProductCategoryID]";
            
            var result =
                _businessManager
                    .CallStoredProcedure<InputGetProductListByProductCategoryId,
                        OutputGetProductListByProductCategoryId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

    }
}
