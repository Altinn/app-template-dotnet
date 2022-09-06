using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Altinn.App;
using Altinn.App.Generated.Model;
using Altinn.App.IntegrationTests;
using App.IntegrationTests.Utils;

using Xunit;

namespace App.IntegrationTestsRef.ApiTests
{
    public class InstanceApiPlatformFailsTest : IClassFixture<CustomWebApplicationFactory<AppModel>>
    {
        private readonly CustomWebApplicationFactory<AppModel> _factory;

        public InstanceApiPlatformFailsTest(CustomWebApplicationFactory<AppModel> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Instance_Post_Instance_FailOk()
        {
            string token = PrincipalUtil.GetToken(1337);

            HttpClient client = SetupUtil.GetTestClient(_factory, "tdd", "platform-fails");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/tdd/platform-fails/instances?instanceOwnerPartyId=1337");

            HttpResponseMessage response = await client.SendAsync(httpRequestMessage);

            string responseContent = await response.Content.ReadAsStringAsync();           

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);            
        }
    }
}
