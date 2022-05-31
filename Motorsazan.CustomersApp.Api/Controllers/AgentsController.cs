using Motorsazan.CustomersApp.Api.Business;
using Motorsazan.CustomersApp.Shared.Models.Input.Agents;
using Motorsazan.CustomersApp.Shared.Models.Output.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Motorsazan.CustomersApp.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    [RoutePrefix("Agents")]
    public class AgentsController : ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        /// دریافت لیست شهر های نمایندگان
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("GetAgentCityNameList")]
        [HttpPost]
        public IHttpActionResult GetAgentCityNameList(
            )
        {
            const string storedProcedureName = "[Sale].[prc_GetAgentCityNameList]";

            var result =
                _businessManager
                    .CallStoredProcedure<
                        OutputGetAgentCityNameList[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        /// دریافت لیست نمایندگان براساس شهر
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("GetAgentListByAgentCityName")]
        [HttpPost]
        public IHttpActionResult GetAgentListByAgentCityName(InputGetAgentListByAgentCityName input)
        {
            const string storedProcedureName = "[Sale].[prc_GetAgentListByAgentCityName]";

            var result =
                _businessManager
                    .CallStoredProcedure< InputGetAgentListByAgentCityName,
                        OutputGetAgentListByAgentCityName[]>(
                        storedProcedureName,input);

            return Ok(result);
        }
    }
}