using System;
using System.Threading.Tasks;
using Altinn.App.Core.Interface;
using Altinn.App.PlatformServices.Interface;
using Altinn.App.Services.Interface;
using Altinn.App.Services.Models.Validation;
using Altinn.Platform.Storage.Interface.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace App.IntegrationTests.Mocks.Apps.Ttd.Externalprefil
{
    /// <summary>
    /// Represents the core logic of an App
    /// </summary>
    public class App : IAppModel
    {
        private readonly ILogger<App> _logger;

        /// <summary>
        /// Initialize a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="logger">A logger from the built in LoggingFactory.</param>
        public App(ILogger<App> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public object Create(string classRef)
        {
            _logger.LogInformation($"CreateNewAppModel {classRef}");

            Type appType = Type.GetType(classRef);
            return Activator.CreateInstance(appType);
        }

        /// <inheritdoc />
        public Type GetModelType(string classRef)
        {
            _logger.LogInformation($"GetAppModelType {classRef}");

            return Type.GetType(classRef);
        }
    }
}
