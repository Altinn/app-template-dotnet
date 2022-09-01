using System;
using Altinn.App.Core.Interface;
using Microsoft.Extensions.Logging;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace App.IntegrationTests.Mocks.Apps.tdd.complex_process
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
    public class AppModel : IAppModel
    {
        private readonly ILogger<AppModel> _logger;
    
        /// <summary>
        /// Initialize new instance of AppModel
        /// </summary>
        /// <param name="logger">Logger for AppModle</param>
        public AppModel(ILogger<AppModel> logger)
        {
            _logger = logger;
        }
    
        /// <inheritdoc />
        public object Create(string classRef)
        {
            _logger.LogInformation($"CreateNewAppModel {classRef}");

            return Activator.CreateInstance(GetModelType(classRef));
        }

        /// <inheritdoc />
        public Type GetModelType(string classRef)
        {
            _logger.LogInformation($"GetAppModelType {classRef}");

            return Type.GetType(classRef);
        }
    }
}