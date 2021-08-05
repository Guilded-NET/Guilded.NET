using Newtonsoft.Json;

namespace Guilded.NET.Base.Users
{
    /// <summary>
    /// Profile flair information.
    /// </summary>
    public class FlairInfo : BaseObject
    {
        /// <summary>
        /// Type of this flair.
        /// </summary>
        /// <remarks>
        /// <para>The type of the flair. Guilded claims that it has secret flairs, so not to crash Guilded.NET, this is temporarily going to be a string.</para>
        /// <para>Incomplete list of flairs:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <term>gil_gang</term>
        ///         <description>This user is part of Gil Gang. Semi-secret flair.</description>
        ///     </item>
        ///     <item>
        ///         <term>guilded_gold_1</term>
        ///         <description>This user has used Guilded Gold v1 and supported Guilded in earlier days.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        /// <value>Flair type</value>
        [JsonProperty(Required = Required.Always)]
        public string Flair
        {
            get; set;
        }
        /// <summary>
        /// Amount of the flairs user has.
        /// </summary>
        /// <remarks>
        /// Count of how many times user has this flair. This is only used by <c>guilded_gold_1</c>.
        /// </remarks>
        /// <value>Flair count</value>
        [JsonProperty(Required = Required.Always)]
        public uint Amount
        {
            get; set;
        }
    }
}