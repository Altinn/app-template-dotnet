using System;

namespace Altinn.App.Api.Models
{
    /// <summary>
    /// A simplified instance model used for presentation of key instance information.
    /// </summary>
    public class LayoutSets
    {
        /// <summary>
        /// The layout set id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The data type bound to the layout set
        /// </summary>
        public string DataType { get; set; }
    }
}