using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guilded.NET.Objects {
    using Teams;
    /// <summary>
    /// A team user is in.
    /// </summary>
    public class UserTeam: BaseTeam {
        /// <summary>
        /// Gets this user team as a normal team.
        /// </summary>
        /// <returns>Team</returns>
        public async Task<Team> AsTeamAsync() =>
            await ParentClient.GetTeamAsync(Id);
        /// <summary>
        /// Gets this user team as a normal team.
        /// </summary>
        /// <returns>Team</returns>
        public Team AsTeam() =>
            ParentClient.GetTeam(Id);
    }
}