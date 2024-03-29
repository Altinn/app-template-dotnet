using System;
using System.IO;
using System.Threading.Tasks;
using Altinn.App.Core.Internal.Pdf;
using Altinn.App.Core.Models;

namespace App.IntegrationTestsRef.Mocks.Services
{
    public class PDFMockSI : IPDF
    {
        public Task<Stream> GeneratePDF(PDFContext pdfContext)
        {
            string unitTestFolder = Path.GetDirectoryName(new Uri(typeof(PDFMockSI).Assembly.Location).LocalPath);
            string dataPath = Path.Combine(unitTestFolder, @"../../../Data/Files/print.pdf");

            Stream ms = new MemoryStream();
            using (FileStream file = new FileStream(dataPath, FileMode.Open, FileAccess.Read))
            {
                file.CopyTo(ms);
            }

            return Task.FromResult(ms);
        }
    }
}
