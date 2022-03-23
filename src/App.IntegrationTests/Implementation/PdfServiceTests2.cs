using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Altinn.App.PlatformServices.Implementation;
using Altinn.App.PlatformServices.Options;
using Altinn.App.Services.Configuration;
using Altinn.App.Services.Implementation;
using Altinn.App.Services.Interface;
using Altinn.App.Services.Models;
using Altinn.Platform.Profile.Models;
using Altinn.Platform.Register.Enums;
using Altinn.Platform.Storage.Interface.Models;
using App.IntegrationTests.Utils;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace App.IntegrationTestsRef.Implementation
{
    public class PdfServiceTests2
    {
        [Fact]
        public async Task GenerateAndStorePdf_MultipleMappingsWithSameOptionsId_ShouldPassCorrectOptionsData()
        {
            // Arrange
            Instance instance = GetInstance();
            DataElement dataElement = GetDataElement();

            string postedPdfContextJson = string.Empty;
            PdfService pdfService = BuildPdfService((HttpRequestMessage requestMessage, CancellationToken cancellationToken) =>
            {
                postedPdfContextJson = requestMessage.Content.ReadAsStringAsync(cancellationToken).Result;
            });

            // Act
            await pdfService.GenerateAndStoreReceiptPDF(instance, "Task_1", dataElement, typeof(IntegrationTests.Mocks.Apps.Ttd.DynamicOptions2.Models.Flyttemelding));

            // Assert
            var pdfContext = JsonSerializer.Deserialize<PDFContext>(postedPdfContextJson, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            pdfContext.OptionsDictionary["fylker"].Values.Should().Contain("46");
            pdfContext.OptionsDictionary["kommuner"].Keys.Count.Should().Be(84);
            pdfContext.OptionsDictionary["kommuner"].Values.Should().Contain("4640");
            pdfContext.OptionsDictionary["kommuner"].Keys.Should().Contain("Sogndal");
            pdfContext.OptionsDictionary["kommuner"].Values.Should().Contain("1813");
            pdfContext.OptionsDictionary["kommuner"].Keys.Should().Contain("Brønnøy");
        }

        private static Instance GetInstance()
        {
            return new Instance()
            {
                Id = "1337/50368f87-3b95-4702-9ff7-3e0eb8501883",
                InstanceOwner = new InstanceOwner()
                {
                    PartyId = "1337",
                    PersonNumber = "01039012345"
                },
                AppId = "tdd/dynamic-options-2",
                Org = "tdd",
                Process = new ProcessState()
                {
                    Started = new DateTime(2022, 3, 21, 13, 41, 5),
                    StartEvent = "StartEvent_1",
                    Ended = new DateTime(2022, 3, 21, 13, 41, 7),
                    EndEvent = "EndEvent_1"
                }
            };
        }

        private static DataElement GetDataElement()
        {
            return new DataElement()
            {
                Id = "9eac88a2-1060-4b86-aba6-3b39bcbad29f",
                DataType = "Flyttemelding",
                ContentType = "application/xml",
                Size = 0,
                Locked = true,
                IsRead = true,
                Tags = new List<string>(),
                LastChanged = new DateTime(2022, 3, 21, 13, 41, 6)
            };
        }

        private static IntegrationTests.Mocks.Apps.Ttd.DynamicOptions2.Models.Flyttemelding GetFormData()
        {
            return new IntegrationTests.Mocks.Apps.Ttd.DynamicOptions2.Models.Flyttemelding()
            {
                FlytterFra = new IntegrationTests.Mocks.Apps.Ttd.DynamicOptions2.Models.FylkeKommune() { Fylke = "18", Kommune = "1813" },
                FlytterTil = new IntegrationTests.Mocks.Apps.Ttd.DynamicOptions2.Models.FylkeKommune() { Fylke = "46", Kommune = "4640" }
            };
        }

        private static PdfService BuildPdfService(Action<HttpRequestMessage, CancellationToken> onDataPostCallback)
        {
            PDFClient pdfClient = MockPdfClient(onDataPostCallback);
            AppResourcesSI appResources = BuildAppResourcesService();
            Mock<IData> dataClient = MockDataClient();
            Mock<IHttpContextAccessor> httpContextAccessor = MockUserInHttpContext();
            Mock<IProfile> profileClient = MockProfileClient();
            var registerClient = new Mock<IRegister>();
            var customPdfHandler = new NullPdfHandler();

            var pdfService = new PdfService(pdfClient, appResources, dataClient.Object, httpContextAccessor.Object, profileClient.Object, registerClient.Object, customPdfHandler);

            return pdfService;
        }

        private static PDFClient MockPdfClient(Action<HttpRequestMessage, CancellationToken> onDataPostCallback)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Content = new StreamContent(new MemoryStream())
                })
                .Callback(onDataPostCallback)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var platformSettings = new PlatformSettings()
            {
                ApiPdfEndpoint = @"http://localhost/not-in-use"
            };
            var platformOptions = Options.Create(platformSettings);
            var pdfClient = new PDFClient(platformOptions, httpClient);

            return pdfClient;
        }

        private static AppResourcesSI BuildAppResourcesService()
        {
            var appSettings = new AppSettings()
            {
                AppBasePath = SetupUtil.GetAppPath("tdd", "dynamic-options-2")
            };
            var appOptions = Options.Create(appSettings);

            var appOptionsFactory = new AppOptionsFactory(new List<IAppOptionsProvider>()
                {
                    new IntegrationTests.Mocks.Apps.Ttd.DynamicOptions2.Options.CommuneAppOptionsProvider(new AppOptionsFileHandler(appOptions)),
                    new DefaultAppOptionsProvider(new AppOptionsFileHandler(appOptions))
                });
            var instanceAppOptionsFactory = new InstanceAppOptionsFactory(new List<IInstanceAppOptionsProvider>());
            var appOptionsService = new AppOptionsService(appOptionsFactory, instanceAppOptionsFactory);
            var appResources = new AppResourcesSI(appOptions, null, null, appOptionsService);

            return appResources;
        }

        private static Mock<IData> MockDataClient()
        {
            IntegrationTests.Mocks.Apps.Ttd.DynamicOptions2.Models.Flyttemelding formData = GetFormData();

            var dataClient = new Mock<IData>();
            dataClient.Setup(s => s.GetFormData(It.IsAny<Guid>(), It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<Guid>())).ReturnsAsync(() => formData);

            return dataClient;
        }

        private static Mock<IHttpContextAccessor> MockUserInHttpContext()
        {
            var user = PrincipalUtil.GetUserPrincipal(1313);
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(s => s.HttpContext.User).Returns(user);

            return httpContextAccessor;
        }

        private static Mock<IProfile> MockProfileClient()
        {
            var userProfile = new UserProfile()
            {
                UserId = 1337,
                UserName = "SophieDDG",
                PhoneNumber = "90001337",
                Email = "1337@altinnstudiotestusers.com",
                PartyId = 1337,
                Party = new Altinn.Platform.Register.Models.Party
                {
                    PartyId = 1337,
                    PartyTypeName = PartyType.Person
                },
                UserType = Altinn.Platform.Profile.Enums.UserType.SelfIdentified,
                ProfileSettingPreference = new ProfileSettingPreference()
            };

            var profileClient = new Mock<IProfile>();
            profileClient.Setup(s => s.GetUserProfile(It.IsAny<int>())).ReturnsAsync(() => userProfile);

            return profileClient;
        }
    }
}
