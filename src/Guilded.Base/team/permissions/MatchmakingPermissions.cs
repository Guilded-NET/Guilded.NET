using System;

namespace Guilded.Base.Permissions
{
    /// <summary>
    /// Permissions related to matchmaking.
    /// </summary>
    /// <remarks>
    /// <para>Defines team permissions related to tournaments &amp; scrims.</para>
    /// </remarks>
    [Flags]
    public enum MatchmakingPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
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
        CreateTournaments = 16,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = CreateScrims | RegisterForTournaments | CreateTournaments
        #endregion
    }
}