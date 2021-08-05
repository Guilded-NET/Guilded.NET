using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Tournament and scrims permissions.
    /// </summary>
    [Flags]
    public enum MatchmakingPermissions
    {
        /// <summary>
        /// Allows you to create matchmaking scrims
        /// </summary>
        CreateScrims = 1,
        /// <summary>
        /// Allows you to use the server to create and manage tournaments
        /// </summary>
        RegisterForTournaments = 4,
        /// <summary>
        /// Allows you to register the server for tournaments
        /// </summary>
        CreateTournaments = 16
    }
}