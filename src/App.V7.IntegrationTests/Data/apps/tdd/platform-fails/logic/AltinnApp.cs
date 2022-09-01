using System;
using Altinn.App.Core.Interface;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace App.IntegrationTests.Mocks.Apps.tdd.platform_fails
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