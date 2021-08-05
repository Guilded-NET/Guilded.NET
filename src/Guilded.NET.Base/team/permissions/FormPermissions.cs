using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Form and poll permissions.
    /// </summary>
    [Flags]
    public enum FormPermissions
    {
        /// <summary>
        /// Allows you to view all form responses
        /// </summary>
        FormResponses = 2,
        /// <summary>
        /// Allows you to view all poll results
        /// </summary>
        PollResults = 16
    }
}