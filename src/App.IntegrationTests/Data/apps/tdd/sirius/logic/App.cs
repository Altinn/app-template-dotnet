using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Altinn.App.Core.Interface;
using Altinn.App.PlatformServices.Interface;
using Altinn.App.Services.Implementation;
using Altinn.App.Services.Interface;
using Altinn.App.Services.Models.Validation;
using Altinn.Platform.Storage.Interface.Models;

using App.IntegrationTests.Mocks.Apps.tdd.sirius.AppLogic;
using App.IntegrationTests.Mocks.Apps.tdd.sirius.AppLogic.Calculation;
using App.IntegrationTests.Mocks.Apps.tdd.sirius.AppLogic.Validation;
using App.IntegrationTestsRef.Data.apps.tdd.sirius.services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
