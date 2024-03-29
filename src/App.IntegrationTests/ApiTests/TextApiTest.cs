using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Altinn.App;
using Altinn.App.Core.Internal.AppModel;
using Altinn.App.IntegrationTests;
using Altinn.Platform.Storage.Interface.Models;

using App.IntegrationTests.Utils;

using Newtonsoft.Json;
using Xunit;

namespace App.IntegrationTestsRef.ApiTests
{
    public class TextApiTest : IClassFixture<CustomWebApplicationFactory<TestDummy>>
    {
        private readonly CustomWebApplicationFactory<TestDummy> _factory;

        public TextApiTest(CustomWebApplicationFactory<TestDummy> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetText_NB_Ok()
        {
            string token = PrincipalUtil.GetToken(1337);

            HttpClient client = SetupUtil.GetTestClient(_factory, "tdd", "endring-av-navn");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "/tdd/endring-av-navn/api/v1/texts/nb");

            HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetText_InvalidLanguage_BadRequest()
        {
            string token = PrincipalUtil.GetToken(1337);
            HttpClient client = SetupUtil.GetTestClient(_factory, "tdd", "endring-av-navn");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "/tdd/endring-av-navn/api/v1/texts/norsk");

            HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetText_LanguageNotFound_NorskBokmalReturned()
        {
            string expectedLanguage = "nb";

            string token = PrincipalUtil.GetToken(1337);
            HttpClient client = SetupUtil.GetTestClient(_factory, "tdd", "endring-av-navn");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "/tdd/endring-av-navn/api/v1/texts/ru");

            HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
            string responseContent = await response.Content.ReadAsStringAsync();
            TextResource actual = (TextResource)JsonConvert.DeserializeObject(responseContent, typeof(TextResource));

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedLanguage, actual.Language);
        }
    }
}
