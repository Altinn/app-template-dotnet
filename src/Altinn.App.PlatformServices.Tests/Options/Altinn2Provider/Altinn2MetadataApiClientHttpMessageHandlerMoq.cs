#nullable enable
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Altinn.App.PlatformServices.Tests.Options.Altinn2Provider
{
    public class Altinn2MetadataApiClientHttpMessageHandlerMoq : HttpMessageHandler
    {
        // Instrumentation to test that caching works
        public int CallCounter { get; private set; } = 0;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            CallCounter++;

            var url = httpRequestMessage.RequestUri?.ToString() ?? string.Empty;

            if (url.StartsWith("https://www.altinn.no/api/metadata/codelists/serverError"))
            {
                return Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                });
            }

            var stringResult = GetStringResult(url);
            var status = stringResult != null ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            var response = new HttpResponseMessage(status);
            response.Content = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(stringResult ?? string.Empty));
            response.Content.Headers.Remove("Content-Type");
            response.Content.Headers.Add("Content-Type", "application/json; charset=utf-8");

            return Task.FromResult(response);
        }

        private string? GetStringResult(string url)
        {
            return url switch
            {
                "https://www.altinn.no/api/metadata/codelists/ASF_land/2758?language=1044" => "{\"Name\": \"ASF_Land\",\"Version\": 2758,\"Language\": 1044,\"Codes\": [{ \"Code\": \"\", \"Value1\": \"\", \"Value2\": \"\", \"Value3\": \"\" },{\"Code\": \"MONTENEGRO\",\"Value1\": \"MONTENEGRO\",\"Value2\": \"ME\",\"Value3\": \"160\"},{ \"Code\": \"NORGE\", \"Value1\": \"NORGE\", \"Value2\": \"NO\", \"Value3\": \"000\" },{\"Code\": \"NY-KALEDONIA\",\"Value1\": \"NY-KALEDONIA\",\"Value2\": \"NC\",\"Value3\": \"833\"},{ \"Code\": \"OMAN\", \"Value1\": \"OMAN\", \"Value2\": \"OM\", \"Value3\": \"520\" },{\"Code\": \"PAKISTAN\",\"Value1\": \"PAKISTAN\",\"Value2\": \"PK\",\"Value3\": \"534\"},{ \"Code\": \"PALAU\", \"Value1\": \"PALAU\", \"Value2\": \"PW\", \"Value3\": \"839\" },{ \"Code\": \"PANAMA\", \"Value1\": \"PANAMA\", \"Value2\": \"PA\", \"Value3\": \"668\" },{\"Code\": \"PAPUA NY-GUINEA\",\"Value1\": \"PAPUA NY-GUINEA\",\"Value2\": \"PG\",\"Value3\": \"827\"},{\"Code\": \"PARAGUAY\",\"Value1\": \"PARAGUAY\",\"Value2\": \"PY\",\"Value3\": \"755\"},{ \"Code\": \"PERU\", \"Value1\": \"PERU\", \"Value2\": \"PE\", \"Value3\": \"760\" },{ \"Code\": \"POLEN\", \"Value1\": \"POLEN\", \"Value2\": \"PL\", \"Value3\": \"131\" },{\"Code\": \"PORTUGAL\",\"Value1\": \"PORTUGAL\",\"Value2\": \"PT\",\"Value3\": \"132\"},{\"Code\": \"SLOVAKIA\",\"Value1\": \"SLOVAKIA\",\"Value2\": \"SK\",\"Value3\": \"157\"},{\"Code\": \"SLOVENIA\",\"Value1\": \"SLOVENIA\",\"Value2\": \"SI\",\"Value3\": \"146\"},{ \"Code\": \"VIETNAM\", \"Value1\": \"VIETNAM\", \"Value2\": \"VN\", \"Value3\": \"575\" },{\"Code\": \"WALLIS/FUTUNAØYENE\",\"Value1\": \"WALLIS/FUTUNAØYENE\",\"Value2\": \"WF\",\"Value3\": \"832\"},{ \"Code\": \"ZAMBIA\", \"Value1\": \"ZAMBIA\", \"Value2\": \"ZM\", \"Value3\": \"389\" },{\"Code\": \"ZIMBABWE\",\"Value1\": \"ZIMBABWE\",\"Value2\": \"ZW\",\"Value3\": \"326\"},{\"Code\": \"ØSTERRIKE\",\"Value1\": \"ØSTERRIKE\",\"Value2\": \"AT\",\"Value3\": \"153\"},],\"_links\": {\"self\": {\"href\": \"https://www.altinn.no/api/metadata/codelists/ASF_Land/2758?language=1044\"}}}",
                "https://www.altinn.no/api/metadata/codelists/ASF_Fylker/3063?language=1044" => "{\"Name\":\"ASF_Fylker\",\"Version\":3063,\"Language\":1044,\"Codes\":[{\"Code\":\"\",\"Value1\":\"\",\"Value2\":\"\",\"Value3\":\"\"},{\"Code\":\"Agder\",\"Value1\":\"Agder\",\"Value2\":\"4200\",\"Value3\":\"\"},{\"Code\":\"Akershus - UTGÅTT\",\"Value1\":\"Akershus - UTGÅTT\",\"Value2\":\"0200\",\"Value3\":\"\"},{\"Code\":\"Aust-Agder - UTGÅTT\",\"Value1\":\"Aust-Agder - UTGÅTT\",\"Value2\":\"0900\",\"Value3\":\"\"},{\"Code\":\"Buskerud - UTGÅTT\",\"Value1\":\"Buskerud -UTGÅTT\",\"Value2\":\"0600\",\"Value3\":\"\"},{\"Code\":\"Finnmark - UTGÅTT\",\"Value1\":\"Finnmark - UTGÅTT\",\"Value2\":\"2000\",\"Value3\":\"\"},{\"Code\":\"Hedmark - UTGÅTT\",\"Value1\":\"Hedmark - UTGÅTT\",\"Value2\":\"0400\",\"Value3\":\"\"},{\"Code\":\"Hordaland - UTGÅTT\",\"Value1\":\"Hordaland - UTGÅTT\",\"Value2\":\"1200\",\"Value3\":\"\"},{\"Code\":\"Innlandet\",\"Value1\":\"Innlandet\",\"Value2\":\"3400\",\"Value3\":\"\"},{\"Code\":\"Møre og Romsdal\",\"Value1\":\"Møre og Romsdal\",\"Value2\":\"1500\",\"Value3\":\"\"},{\"Code\":\"Nordland\",\"Value1\":\"Nordland\",\"Value2\":\"1800\",\"Value3\":\"\"},{\"Code\":\"Oppland - UTGÅTT\",\"Value1\":\"Oppland -UTGÅTT\",\"Value2\":\"0500\",\"Value3\":\"\"},{\"Code\":\"Oslo\",\"Value1\":\"Oslo\",\"Value2\":\"0300\",\"Value3\":\"\"},{\"Code\":\"Rogaland\",\"Value1\":\"Rogaland\",\"Value2\":\"1100\",\"Value3\":\"\"},{\"Code\":\"Sogn og Fjordane - UTGÅTT\",\"Value1\":\"Sogn og Fjordane - UTGÅTT\",\"Value2\":\"1400\",\"Value3\":\"\"},{\"Code\":\"Telemark - UTGÅTT\",\"Value1\":\"Telemark - UTGÅTT\",\"Value2\":\"0800\",\"Value3\":\"\"},{\"Code\":\"Troms - UTGÅTT\",\"Value1\":\"Troms - UTGÅTT\",\"Value2\":\"1900\",\"Value3\":\"\"},{\"Code\":\"Troms og Finnmark\",\"Value1\":\"Troms og Finnmark\",\"Value2\":\"5400\",\"Value3\":\"\"},{\"Code\":\"Trøndelag\",\"Value1\":\"Trøndelag\",\"Value2\":\"5000\",\"Value3\":\"\"},{\"Code\":\"Vest-Agder - UTGÅTT\",\"Value1\":\"Vest-Agder - UTGÅTT\",\"Value2\":\"1000\",\"Value3\":\"\"},{\"Code\":\"Vestfold og Telemark\",\"Value1\":\"Vestfold og Telemark\",\"Value2\":\"3800\",\"Value3\":\"\"},{\"Code\":\"Vestfold - UTGÅTT\",\"Value1\":\"Vestfold -UTGÅTT\",\"Value2\":\"0700\",\"Value3\":\"\"},{\"Code\":\"Vestland\",\"Value1\":\"Vestland\",\"Value2\":\"4600\",\"Value3\":\"\"},{\"Code\":\"Viken\",\"Value1\":\"Viken\",\"Value2\":\"3000\",\"Value3\":\"\"},{\"Code\":\"Østfold - UTGÅTT\",\"Value1\":\"Østfold - UTGÅTT\",\"Value2\":\"0100\",\"Value3\":\"\"}],\"_links\":{\"self\":{\"href\":\"https://www.altinn.no/api/metadata/codelists/ASF_Fylker/3063?language=1044\"}}}",
                "https://www.altinn.no/api/metadata/codelists/ASF_Fylker/3063?language=2068" => "{\"Name\":\"ASF_Fylker\",\"Version\":3063,\"Language\":1044,\"Codes\":[{\"Code\":\"\",\"Value1\":\"\",\"Value2\":\"\",\"Value3\":\"\"},{\"Code\":\"Agder\",\"Value1\":\"Agder\",\"Value2\":\"4200\",\"Value3\":\"\"},{\"Code\":\"Akershus - UTGÅTT\",\"Value1\":\"Akershus - UTGÅTT\",\"Value2\":\"0200\",\"Value3\":\"\"},{\"Code\":\"Aust-Agder - UTGÅTT\",\"Value1\":\"Aust-Agder - UTGÅTT\",\"Value2\":\"0900\",\"Value3\":\"\"},{\"Code\":\"Buskerud - UTGÅTT\",\"Value1\":\"Buskerud -UTGÅTT\",\"Value2\":\"0600\",\"Value3\":\"\"},{\"Code\":\"Finnmark - UTGÅTT\",\"Value1\":\"Finnmark - UTGÅTT\",\"Value2\":\"2000\",\"Value3\":\"\"},{\"Code\":\"Hedmark - UTGÅTT\",\"Value1\":\"Hedmark - UTGÅTT\",\"Value2\":\"0400\",\"Value3\":\"\"},{\"Code\":\"Hordaland - UTGÅTT\",\"Value1\":\"Hordaland - UTGÅTT\",\"Value2\":\"1200\",\"Value3\":\"\"},{\"Code\":\"Innlandet\",\"Value1\":\"Innlandet\",\"Value2\":\"3400\",\"Value3\":\"\"},{\"Code\":\"Møre og Romsdal\",\"Value1\":\"Møre og Romsdal\",\"Value2\":\"1500\",\"Value3\":\"\"},{\"Code\":\"Nordland\",\"Value1\":\"Nordland\",\"Value2\":\"1800\",\"Value3\":\"\"},{\"Code\":\"Oppland - UTGÅTT\",\"Value1\":\"Oppland -UTGÅTT\",\"Value2\":\"0500\",\"Value3\":\"\"},{\"Code\":\"Oslo\",\"Value1\":\"Oslo\",\"Value2\":\"0300\",\"Value3\":\"\"},{\"Code\":\"Rogaland\",\"Value1\":\"Rogaland\",\"Value2\":\"1100\",\"Value3\":\"\"},{\"Code\":\"Sogn og Fjordane - UTGÅTT\",\"Value1\":\"Sogn og Fjordane - UTGÅTT\",\"Value2\":\"1400\",\"Value3\":\"\"},{\"Code\":\"Telemark - UTGÅTT\",\"Value1\":\"Telemark - UTGÅTT\",\"Value2\":\"0800\",\"Value3\":\"\"},{\"Code\":\"Troms - UTGÅTT\",\"Value1\":\"Troms - UTGÅTT\",\"Value2\":\"1900\",\"Value3\":\"\"},{\"Code\":\"Troms og Finnmark\",\"Value1\":\"Troms og Finnmark\",\"Value2\":\"5400\",\"Value3\":\"\"},{\"Code\":\"Trøndelag\",\"Value1\":\"Trøndelag\",\"Value2\":\"5000\",\"Value3\":\"\"},{\"Code\":\"Vest-Agder - UTGÅTT\",\"Value1\":\"Vest-Agder - UTGÅTT\",\"Value2\":\"1000\",\"Value3\":\"\"},{\"Code\":\"Vestfold og Telemark\",\"Value1\":\"Vestfold og Telemark\",\"Value2\":\"3800\",\"Value3\":\"\"},{\"Code\":\"Vestfold - UTGÅTT\",\"Value1\":\"Vestfold -UTGÅTT\",\"Value2\":\"0700\",\"Value3\":\"\"},{\"Code\":\"Vestland\",\"Value1\":\"Vestland\",\"Value2\":\"4600\",\"Value3\":\"\"},{\"Code\":\"Viken\",\"Value1\":\"Viken\",\"Value2\":\"3000\",\"Value3\":\"\"},{\"Code\":\"Østfold - UTGÅTT\",\"Value1\":\"Østfold - UTGÅTT\",\"Value2\":\"0100\",\"Value3\":\"\"}],\"_links\":{\"self\":{\"href\":\"https://www.altinn.no/api/metadata/codelists/ASF_Fylker/3063?language=1044\"}}}",
                _ => null,
            };
        }
    }
}