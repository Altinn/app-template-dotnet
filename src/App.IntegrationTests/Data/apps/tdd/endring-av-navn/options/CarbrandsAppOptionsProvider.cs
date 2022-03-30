using Altinn.App.Common.Models;
using Altinn.App.PlatformServices.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.IntegrationTestsRef.Data.apps.tdd.endring_av_navn.options
{
    public class CarbrandsAppOptionsProvider : IAppOptionsProvider
    {
        public string Id => "carbrands";

        private readonly IAppOptionsFileHandler _appOptionsFileHandler;

        public CarbrandsAppOptionsProvider(IAppOptionsFileHandler appOptionsFileHandler)
        {
            _appOptionsFileHandler = appOptionsFileHandler;
        }

        public Task<AppOptions> GetAppOptionsAsync(string language, Dictionary<string, string> keyValuePairs)
        {
            throw new NotImplementedException();
        }
    }
}
