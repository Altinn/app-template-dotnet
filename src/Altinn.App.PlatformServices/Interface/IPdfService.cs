using System;
using System.Threading.Tasks;
using Altinn.App.Services.Models;
using Altinn.Platform.Storage.Interface.Models;

namespace Altinn.App.PlatformServices.Interface
{
    /// <summary>
    /// Interface for handling generation and storing of PDF's
    /// </summary>
    public interface IPdfService
    {
        /// <summary>
        /// Generates the PDF based on the current data and stores it
        /// </summary>
        /// <param name="instance">The instance the PDF is based on.</param>
        /// <param name="taskId">The task id matching the </param>
        /// <param name="dataElement">Reference to the data element.</param>
        /// <param name="dataElementModelType">Type of data referenced</param>        
        Task GenerateAndStoreReceiptPDF(Instance instance, string taskId, DataElement dataElement, Type dataElementModelType);

        /// <summary>
        /// Exposes an object containing all required data in order to produce the PDF.
        /// This is only exposed to show the values used to generate the PDF.
        /// The context is populated and exposed after the PDF is generated.
        /// Primary use is to allow for value checking as part of tests.
        /// </summary>
        PDFContext GetPdfContext();
    }
}