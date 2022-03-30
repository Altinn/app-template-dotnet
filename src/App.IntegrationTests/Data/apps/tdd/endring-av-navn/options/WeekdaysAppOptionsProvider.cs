using System.Collections.Generic;
using System.Threading.Tasks;
using Altinn.App.Common.Models;
using Altinn.App.PlatformServices.Options;

namespace App.IntegrationTests.Mocks.Apps.Ttd.EndringAvNavn.Options
{
    internal class WeekdaysAppOptionsProvider : IAppOptionsProvider
    {
        public string Id => "weekdays";

        public Task<AppOptions> GetAppOptionsAsync(string language, Dictionary<string, string> keyValuePairs)
        {
            var appOptions = new AppOptions();

            appOptions.Options.Add(new AppOption() { Value = "1", Label = "Mandag" });
            appOptions.Options.Add(new AppOption() { Value = "2", Label = "Tirsdag" });
            appOptions.Options.Add(new AppOption() { Value = "3", Label = "Onsdag" });
            appOptions.Options.Add(new AppOption() { Value = "4", Label = "Torsdag" });
            appOptions.Options.Add(new AppOption() { Value = "5", Label = "Fredag" });
            appOptions.Options.Add(new AppOption() { Value = "6", Label = "Lørdag" });
            appOptions.Options.Add(new AppOption() { Value = "7", Label = "Søndag" });

            appOptions.IsCacheable = true;

            return Task.FromResult(appOptions);
        }
    }
}
