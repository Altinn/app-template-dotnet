using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Altinn.App.Common.Models;
using Altinn.App.Services.Interface;

namespace Altinn.App.Services.Implementation
{
    /// <summary>
    /// Interface for page order handling in stateless apps
    /// </summary>
    public class DefaultStatefulPageOrder : IStatefulPageOrder
    {
        private readonly IAltinnApp _altinnApp;

        /// <summary>
        /// Default implementation for page order
        /// </summary>
        /// <param name="resources">AppResources service</param>
        public DefaultStatefulPageOrder(IAltinnApp altinnApp)
        {
            _altinnApp = altinnApp;
        }

        /// <inheritdoc />
        public async Task<List<string>> GetPageOrder(string org, string app, int instanceOwnerId, Guid instanceGuid, string layoutSetId, string currentPage, string dataTypeId, object formData)
        {
            return await _altinnApp.GetPageOrder(org, app, instanceOwnerId, instanceGuid, layoutSetId, currentPage, dataTypeId, formData);
        }
    }
}