using System.Collections.Generic;
using System.Threading.Tasks;
using Altinn.App.PlatformServices.Interface;
using Altinn.Platform.Storage.Interface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Altinn.App.Api.Controllers
{
    /// <summary>
    /// Represents the Application language API giving access to the different languages supported by the application.
    /// </summary>
    [Route("{org}/{app}/api/v1/applicationlanguages")]
    [Authorize]
    public class ApplicationLanguageController : ControllerBase
    {
        private readonly IApplicationLanguage _applicationLanguage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationLanguageController"/> class.
        /// </summary>
        /// <param name="applicationLanguage">An implementation with access to application languages.</param>
        public ApplicationLanguageController(IApplicationLanguage applicationLanguage)
        {
            _applicationLanguage = applicationLanguage;
        }

        /// <summary>
        /// Method to retrieve the supported languages from the application
        /// </summary>
        /// <returns>Returns a list of ApplicationLanguages</returns>
        [HttpGet]
        public async Task<ActionResult<List<ApplicationLanguage>>> GetLanguages()
        {
            return await _applicationLanguage.GetApplicationLanguages();
        }
    }
}
