using System;
using System.Threading.Tasks;
using Altinn.App.Core.Interface;
using Altinn.App.PlatformServices.Interface;
using Altinn.App.Services.Configuration;
using Altinn.App.Services.Implementation;
using Altinn.App.Services.Interface;
using Altinn.Platform.Storage.Interface.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace App.IntegrationTests.Mocks.Apps.tdd.custom_validation
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
    public class AltinnApp : IAppModel
    {
        public object Create(string classRef)
        {
            return Activator.CreateInstance(GetModelType(classRef));
        }

        public Type GetModelType(string classRef)
        {
            return Type.GetType(classRef);
        }
    }
}