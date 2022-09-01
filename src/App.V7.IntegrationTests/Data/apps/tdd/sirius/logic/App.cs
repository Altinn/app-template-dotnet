using System;
using Altinn.App.Core.Interface;
using Microsoft.Extensions.Logging;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace App.IntegrationTests.Mocks.Apps.tdd.sirius
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
    public class App : IAppModel
    {
        private readonly ILogger<App> _logger;

        public App(
            ILogger<App> logger)
        {
            _logger = logger;
        }

        public object Create(string classRef)
        {
            _logger.LogInformation($"CreateNewAppModel {classRef}");

            Type appType = Type.GetType(classRef);
            return Activator.CreateInstance(appType);
        }

        public Type GetModelType(string classRef)
        {
            _logger.LogInformation($"GetAppModelType {classRef}");

            return Type.GetType(classRef);
        }
    }
}