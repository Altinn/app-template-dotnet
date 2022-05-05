using System.Collections.Generic;
using System.Threading.Tasks;
using Altinn.App.Services.Interface;
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
        private readonly IAppResources _appResources;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationLanguageController"/> class.
        /// </summary>
        /// <param name="appResources">A service with access to text resources.</param>
        public ApplicationLanguageController(IAppResources appResources)
        {
            _appResources = appResources;
        }

        /// <summary>
        /// Method to retrieve the supported languages from the application
        /// </summary>
        /// <returns>Returns a dictionary where the key is the language code represented in a 2-char ISOFormat
        /// and the value is the language</returns>
        [HttpGet]
        public async Task<ActionResult<List<ApplicationLanguage>>> GetLanguages()
        {
            return await _appResources.GetApplicationLanguages();
        }
    }
}
