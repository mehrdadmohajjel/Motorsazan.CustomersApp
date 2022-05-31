using Motorsazan.CustomersApp.Api.Business;
using Motorsazan.CustomersApp.Api.Filters;
using Motorsazan.CustomersApp.Shared.Models.Input.NotificationSlider;
using Motorsazan.CustomersApp.Shared.Models.Output.NotificationSlider;
using System.Web.Http;

namespace Motorsazan.CustomersApp.Api.Controllers
{
    [RoutePrefix("NotificationSlider")]
    public class NotificationSliderController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();


        /// <summary>
        ///     دریافت آخرین خبر
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetLastNews")]
        [HttpPost]
        public IHttpActionResult GetLastNews()
        {
            const string storedProcedureName = "[Sale].[prc_GetLastNews]";

            var result =
                _businessManager
                    .CallStoredProcedure<
                        OutputGetLastNews[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت 5اعلان آخر
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetLastFiveNews")]
        [HttpPost]
        public IHttpActionResult GetLastFiveNews()
        {
            const string storedProcedureName = "[Sale].[prc_GetLastFiveNews]";

            var result =
                _businessManager
                    .CallStoredProcedure<
                        OutputGetLastNews[]>(
                        storedProcedureName);

            return Ok(result);
        }


        /// <summary>
        ///     دریافت جزئیات اعلان با شناسه اعلان
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetNotificationSliderListByNotificationSliderId")]
        [HttpPost]
        public IHttpActionResult GetNotificationSliderListByNotificationSliderId(
            InputGetNotificationSliderListByNotificationSliderId input)
        {
            const string storedProcedureName = "[Sale].[prc_GetNotificationSliderListByNotificationSliderID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetNotificationSliderListByNotificationSliderId,
                        OutputGetNotificationSliderListByNotificationSliderId>(
                        storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        ///     دریافت عکس با عنوان
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetNotificationSliderAttachmentFileByFileName")]
        [HttpPost]
        public IHttpActionResult GetNotificationSliderAttachmentFileByFileName(
            InputGetNotificationSliderAttachmentFileByFileName input)
        {
            const string storedProcedureName = "[Sale].[prc_GetNotificationSliderAttachmentFileByFileName]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetNotificationSliderAttachmentFileByFileName,
                        OutputGetNotificationSliderAttachmentFileByFileName>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}