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
    public class DefaultStatelessPageOrder : IStatelessPageOrder
    {
        private readonly IAppResources _resources;

        /// <summary>
        /// Default implementation for page order
        /// </summary>
        /// <param name="resources">AppResources service</param>
        public DefaultStatelessPageOrder(IAppResources resources)
        {
            _resources = resources;
        }

        /// <inheritdoc />
        public async Task<List<string>> GetPageOrder(string org, string app, string layoutSetId, string currentPage, string dataTypeId, object formData)
        {
            LayoutSettings layoutSettings = null;

            if (string.IsNullOrEmpty(layoutSetId))
            {
                layoutSettings = _resources.GetLayoutSettings();
            }
            else
            {
                layoutSettings = _resources.GetLayoutSettingsForSet(layoutSetId);
            }

            return await Task.FromResult(layoutSettings.Pages.Order);
        }
    }
}