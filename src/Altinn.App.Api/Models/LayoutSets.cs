using System;
using System.Collections.Generic;

namespace Altinn.App.Api.Models
{
    /// <summary>
    /// A definition of the layout sets available to an app
    /// </summary>
    public class LayoutSets
    {
        /// <summary>
        /// The list of layout sets
        /// </summary>
        public List<LayoutSet> Sets { get; set; }
    }

    /// <summary>
    /// A layout set definition
    /// </summary>
    public class LayoutSet
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
