using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Altinn.App.Services.Interface
{
    /// <summary>
    /// Interface for page order handling in stateless apps
    /// </summary>
    public interface IStatelessPageOrder
    {
        /// <summary>
        /// Gets the current page order of the app
        /// </summary>
        /// <param name="org">The app owner.</param>
        /// <param name="app">The app.</param>
        /// <param name="layoutSetId">The layout set id</param>
        /// <param name="currentPage">The current page of the instance.</param>
        /// <param name="dataTypeId">The data type id of the current layout.</param>
        /// <param name="formData">The form data.</param>
        /// <returns> The pages in sorted order.</returns>
        Task<List<string>> GetPageOrder(string org, string app, string layoutSetId, string currentPage, string dataTypeId, object formData);
    }
}