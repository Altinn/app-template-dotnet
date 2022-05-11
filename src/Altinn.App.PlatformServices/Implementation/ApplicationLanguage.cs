using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Altinn.App.PlatformServices.Helpers;
using Altinn.App.PlatformServices.Interface;
using Altinn.App.Services.Configuration;
using Altinn.App.Services.Implementation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Altinn.App.PlatformServices.Implementation
{
    /// <summary>
    /// An implementation used to retrieve the supported application languages.
    /// </summary>
    public class ApplicationLanguage : IApplicationLanguage
    {
        private readonly AppSettings _settings;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationLanguage"/> class.
        /// </summary>
        /// <param name="settings">The app repository settings.</param>
        /// <param name="logger">A logger from the built in logger factory.</param>
        public ApplicationLanguage(
            IOptions<AppSettings> settings,
            ILogger<AppResourcesSI> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<List<Platform.Storage.Interface.Models.ApplicationLanguage>> GetApplicationLanguages()
        {
            string pathTextsResourceFolder = _settings.AppBasePath + _settings.ConfigurationFolder + _settings.TextFolder;

            DirectoryInfo directoryInfo = new DirectoryInfo(pathTextsResourceFolder);

            if (!directoryInfo.Exists)
            {
                _logger.LogWarning("The text resource directory does not exist");
                return new List<Platform.Storage.Interface.Models.ApplicationLanguage>();
            }

            if (directoryInfo.GetFiles().Length < 1)
            {
                _logger.LogWarning("There are no resource files located in the text resource directory");
                return new List<Platform.Storage.Interface.Models.ApplicationLanguage>();
            }

            var textResourceFilesInDirectory = directoryInfo.GetFiles();
            var applicationLanguages = new List<Platform.Storage.Interface.Models.ApplicationLanguage>();

            foreach (var fileInfo in textResourceFilesInDirectory)
            {
                try
                {
                    string fullFileName = Path.Join(pathTextsResourceFolder, fileInfo.Name);
                    PathHelper.EnsureLegalPath(pathTextsResourceFolder, fullFileName);
                    if (!File.Exists(fullFileName))
                    {
                        _logger.LogWarning("Something went wrong while trying to fetch the application language");
                        return new List<Platform.Storage.Interface.Models.ApplicationLanguage>();
                    }

                    Platform.Storage.Interface.Models.ApplicationLanguage applicationLanguage;

                    await using (FileStream fileStream = new(fullFileName, FileMode.Open, FileAccess.Read))
                    {
                        JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                        applicationLanguage = await System.Text.Json.JsonSerializer.DeserializeAsync<Platform.Storage.Interface.Models.ApplicationLanguage>(fileStream, options);
                    }

                    applicationLanguages.Add(applicationLanguage);
                }
                catch (Exception)
                {
                    _logger.LogWarning("Something went wrong while trying to fetch the application language");
                    return new List<Platform.Storage.Interface.Models.ApplicationLanguage>();
                }
            }

            return applicationLanguages;
        }
    }
}
