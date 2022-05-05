using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

using Altinn.App.Common.Models;
using Altinn.App.Common.Serialization;
using Altinn.App.PlatformServices.Models;
using Altinn.App.Services.Implementation;
using Altinn.App.Services.Interface;
using Altinn.Platform.Storage.Interface.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Altinn.App.Api.Controllers
{
    /// <summary>
    /// Handles page related operations
    /// </summary>
    [ApiController]
    [Route("{org}/{app}/v1/pages")]
    public class StatelessPagesController : ControllerBase
    {
        private readonly IAltinnApp _altinnApp;
        private readonly IAppResources _resources;
        private readonly IPageOrder _pageOrder;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagesController"/> class.
        /// </summary>
        /// <param name="altinnApp">The current App Core used to interface with custom logic</param>
        /// <param name="resources">The app resource service</param>
        /// <param name="pageOrder">The page order service</param>
        /// <param name="logger">A logger provided by the logging framework.</param>
        public StatelessPagesController(IAltinnApp altinnApp, IAppResources resources, IPageOrder pageOrder, ILogger<PagesController> logger)
        {
            _altinnApp = altinnApp;
            _resources = resources;
            _pageOrder = pageOrder;
            _logger = logger;
        }

        /// <summary>
        /// Get the page order based on the current state of the instance
        /// </summary>
        /// <returns>The pages sorted in the correct order</returns>
        [HttpPost("order")]
        public async Task<ActionResult<List<string>>> GetPageOrder(
            [FromRoute] string org,
            [FromRoute] string app,
            [FromQuery] string layoutSetId,
            [FromQuery] string currentPage,
            [FromQuery] string dataTypeId,
            [FromBody] dynamic formData)
        {
            if (string.IsNullOrEmpty(dataTypeId))
            {
                return BadRequest($"Query parameter `{nameof(dataTypeId)}` must be defined");
            }

            string classRef = _resources.GetClassRefForLogicDataType(dataTypeId);

            object data = JsonConvert.DeserializeObject(formData.ToString(), _altinnApp.GetAppModelType(classRef));
            return await _pageOrder.GetPageOrder(new AppIdentifier(org, app), InstanceIdentifier.NoInstance, layoutSetId, currentPage, dataTypeId, data);
        }
    }
}
