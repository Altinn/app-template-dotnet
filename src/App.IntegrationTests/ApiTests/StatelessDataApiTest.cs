using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Altinn.App;
using Altinn.App.Core.Internal.AppModel;
using Altinn.App.IntegrationTests;
using App.IntegrationTests.Utils;
using Newtonsoft.Json;

using Xunit;

namespace App.IntegrationTests.ApiTests
{
    public class StatelessDataApiTest : IClassFixture<CustomWebApplicationFactory<TestDummy>>
    {
        private readonly CustomWebApplicationFactory<TestDummy> _factory;

        public StatelessDataApiTest(CustomWebApplicationFactory<TestDummy> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task StatelessData_Get_ObjectSucessfullyPrefilledAndCalculated()
        {
            // Arrange
            string org = "ttd";
            string app = "presentationfields-app";
            string expectedPrefillValue = "Sophie Salt";
            string expectedCalculatedValue = "calculatedValue";
            string token = PrincipalUtil.GetToken(1337);

            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestUri = $"/{org}/{app}/v1/data?dataType=default";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);

            string responseContent = await res.Content.ReadAsStringAsync();
            Mocks.Apps.Ttd.PresentationTextsApp.Skjema dataObject = JsonConvert.DeserializeObject<Mocks.Apps.Ttd.PresentationTextsApp.Skjema>(responseContent);
            string actualPrefillValue = dataObject?.OpplysningerOmArbeidstakerengrp8819?.Arbeidsforholdgrp8856?.AnsattSammenhengendeAnsattAnsettelsedatadef33267?.value;
            string actualCalculatedValue = dataObject?.OpplysningerOmArbeidstakerengrp8819?.Skjemainstansgrp8854?.IdentifikasjonsnummerKravdatadef33317?.value;

            // Assert
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.NotNull(actualPrefillValue);
            Assert.NotNull(actualCalculatedValue);
            Assert.Equal(expectedPrefillValue, actualPrefillValue);
            Assert.Equal(expectedCalculatedValue, actualCalculatedValue);
        }

        [Fact]
        public async Task GetAnonymous_CorrectDataType_ShouldReturnOk()
        {
            // Arrange
            string org = "ttd";
            string app = "anonymous-stateless";
            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);
            string requestUri = $"/{org}/{app}/v1/data/anonymous?dataType=Veileder";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);
            string responseContent = await res.Content.ReadAsStringAsync();
            Mocks.Apps.Ttd.AnonymousStateless.Models.Veileder dataObject = JsonConvert.DeserializeObject<Mocks.Apps.Ttd.AnonymousStateless.Models.Veileder>(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Theory]
        [InlineData("nonexisting")]
        [InlineData("")]
        public async Task GetAnonymous_InvalidDataType_ShouldReturnBadRequest(string dataType)
        {
            // Arrange
            string org = "ttd";
            string app = "anonymous-stateless";
            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);
            string requestUri = $"/{org}/{app}/v1/data/anonymous?dataType={dataType}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
        }

        [Fact]
        public async Task StatelessData_Get_WithPartyHeader_ObjectSucessfullyPrefilledAndCalculated()
        {
            // Arrange
            string org = "ttd";
            string app = "presentationfields-app";
            string expectedPrefillValue = "Sophie Salt";
            string expectedCalculatedValue = "calculatedValue";
            string token = PrincipalUtil.GetToken(1337);

            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestUri = $"/{org}/{app}/v1/data?dataType=default";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            httpRequestMessage.Headers.Add("party", "partyid:1337");

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);

            string responseContent = await res.Content.ReadAsStringAsync();
            Mocks.Apps.Ttd.PresentationTextsApp.Skjema dataObject = JsonConvert.DeserializeObject<Mocks.Apps.Ttd.PresentationTextsApp.Skjema>(responseContent);
            string actualPrefillValue = dataObject?.OpplysningerOmArbeidstakerengrp8819?.Arbeidsforholdgrp8856?.AnsattSammenhengendeAnsattAnsettelsedatadef33267?.value;
            string actualCalculatedValue = dataObject?.OpplysningerOmArbeidstakerengrp8819?.Skjemainstansgrp8854?.IdentifikasjonsnummerKravdatadef33317?.value;

            // Assert
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.NotNull(actualPrefillValue);
            Assert.NotNull(actualCalculatedValue);
            Assert.Equal(expectedPrefillValue, actualPrefillValue);
            Assert.Equal(expectedCalculatedValue, actualCalculatedValue);
        }

        [Fact]
        public async Task StatelessData_Get_WithPartyHeaderSSN_ObjectSucessfullyPrefilledAndCalculated()
        {
            // Arrange
            string org = "ttd";
            string app = "presentationfields-app";
            string expectedPrefillValue = "Sophie Salt";
            string expectedCalculatedValue = "calculatedValue";
            string token = PrincipalUtil.GetToken(1337);

            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestUri = $"/{org}/{app}/v1/data?dataType=default";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            httpRequestMessage.Headers.Add("party", "person:01039012345");

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);

            string responseContent = await res.Content.ReadAsStringAsync();
            Mocks.Apps.Ttd.PresentationTextsApp.Skjema dataObject = JsonConvert.DeserializeObject<Mocks.Apps.Ttd.PresentationTextsApp.Skjema>(responseContent);
            string actualPrefillValue = dataObject?.OpplysningerOmArbeidstakerengrp8819?.Arbeidsforholdgrp8856?.AnsattSammenhengendeAnsattAnsettelsedatadef33267?.value;
            string actualCalculatedValue = dataObject?.OpplysningerOmArbeidstakerengrp8819?.Skjemainstansgrp8854?.IdentifikasjonsnummerKravdatadef33317?.value;

            // Assert
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.NotNull(actualPrefillValue);
            Assert.NotNull(actualCalculatedValue);
            Assert.Equal(expectedPrefillValue, actualPrefillValue);
            Assert.Equal(expectedCalculatedValue, actualCalculatedValue);
        }

        [Fact]
        public async Task StatelessData_Post_MissingDataType()
        {
            // Arrange
            string org = "ttd";
            string app = "presentationfields-app";

            string token = PrincipalUtil.GetToken(1337);

            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestUri = $"/{org}/{app}/v1/data";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
        }

        [Fact]
        public async Task StatelessData_Post_CalculationsRunAndDataReturned()
        {
            // Arrange
            string org = "ttd";
            string app = "model-validation";
            decimal expected = 1001;

            string token = PrincipalUtil.GetToken(1337);

            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestUri = $"/{org}/{app}/v1/data?dataType=default";
            string requestBody = "{\"skjemanummer\":\"1472\",\"spesifikasjonsnummer\":\"9812\",\"blankettnummer\":\"AFP-01\",\"tittel\":\"ArbeidsgiverskjemaAFP\",\"gruppeid\":\"8818\",\"OpplysningerOmArbeidstakerengrp8819\":{\"Arbeidsforholdgrp8856\":{\"AnsattSammenhengendeAnsattAnsettelsedatadef33267\":{\"value\":\"SophieSalt\",\"orid\":\"33267\"},},\"Skjemainstansgrp8854\":{\"Journalnummerdatadef33316\":{\"value\":\"1000\"}}}}";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);

            string responseContent = await res.Content.ReadAsStringAsync();
            Mocks.Apps.ttd.model_validation.Skjema dataObject = JsonConvert.DeserializeObject<Mocks.Apps.ttd.model_validation.Skjema>(responseContent);
            decimal? actual = dataObject?.OpplysningerOmArbeidstakerengrp8819?.Skjemainstansgrp8854?.Journalnummerdatadef33316?.value;

            // Assert
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PostAnonymous_ValidDatatype_CalculationsRunAndDataReturned()
        {
            // Arrange
            string org = "ttd";
            string app = "anonymous-stateless";
            string expected = "6863";

            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);

            string requestUri = $"/{org}/{app}/v1/data/anonymous?dataType=Veileder";
            string requestBody = "{\"Bransje\":\"A\",\"Kommune\":null}";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);

            string responseContent = await res.Content.ReadAsStringAsync();
            Mocks.Apps.Ttd.AnonymousStateless.Models.Veileder dataObject = JsonConvert.DeserializeObject<Mocks.Apps.Ttd.AnonymousStateless.Models.Veileder>(responseContent);
            string actual = dataObject.Kommune;

            // Assert
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task StatelessData_Post_InvalidDataType()
        {
            // Arrange
            string org = "ttd";
            string app = "model-validation";

            string token = PrincipalUtil.GetToken(1337);

            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestUri = $"/{org}/{app}/v1/data?dataType=tix";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json")
            };

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
        }

        [Theory]
        [InlineData("", "{}")]
        [InlineData("nonexisting", "{}")]
        [InlineData("Veileder", "{\"BransjeX\":\"A\",\"KommuneX\":null}")]
        public async Task PostAnonymous_InvalidDataType_ShouldReturnBadRequest(string dataType, string bodyContent)
        {
            // Arrange
            string org = "ttd";
            string app = "model-validation";

            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);

            string requestUri = $"/{org}/{app}/v1/data/anonymous?dataType={dataType}";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(bodyContent, Encoding.UTF8, "application/json")
            };

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
        }

        [Fact]
        public async Task StatelessData_Post_RequestMissingBodyBadRequest()
        {
            // Arrange
            string org = "ttd";
            string app = "model-validation";

            string token = PrincipalUtil.GetToken(1337);

            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestUri = $"/{org}/{app}/v1/data?dataType=default";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
        }

        [Fact]
        public async Task StatelessData_Get_CalculationsRunAndDataReturned()
        {
            // Arrange
            string org = "ttd";
            string app = "dayplanner";

            string token = PrincipalUtil.GetToken(1337);

            HttpClient client = SetupUtil.GetTestClient(_factory, org, app);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestUri = $"/{org}/{app}/v1/data?dataType=default";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            httpRequestMessage.Headers.Add("party", "org:897069650");

            // Act
            HttpResponseMessage res = await client.SendAsync(httpRequestMessage);

            string responseContent = await res.Content.ReadAsStringAsync();
            Mocks.Apps.Ttd.Dayplanner.MyDay dataObject = JsonConvert.DeserializeObject<Mocks.Apps.Ttd.Dayplanner.MyDay>(responseContent);
 
            // Assert
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.NotNull(dataObject);
        }
    }
}
