using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Altinn.App.Api.Middleware
{
    /// <summary>
    /// Adds a constraint that there user is not authenticated
    /// </summary>
    public class AuthenticatedAttribute : Attribute, IActionConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedAttribute"/> class.
        /// </summary>
        public AuthenticatedAttribute()
        {
        }

        /// <inheritdoc/>
        public int Order => 999;

        /// <inheritdoc/>
        public bool Accept(ActionConstraintContext context)
        {
            if (context.RouteContext.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return true;
            }

            return false;
        }
    }
}
