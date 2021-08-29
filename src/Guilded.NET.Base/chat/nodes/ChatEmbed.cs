using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    using Embeds;
    /// <summary>
    /// Container that holds embeds.
    /// </summary>
    /// <remarks>
    /// Node that holds custom/rich embeds.
    /// </remarks>
    /// <example>
    /// <para>Chat embed with 2 embeds:</para>
    /// <code>
    /// ChatEmbed embed = new ChatEmbed
    /// (
    ///     new Embed("Title", "Description", "Footer"),
    ///     new Embed()
    ///         .AddTitle("Title")
    ///         .AddColor(0xFF0000)
    /// );
    /// </code>
    /// </example>
    /// <seealso cref="Embed"/>
    /// <seealso cref="ContentEmbed"/>
    public class ChatEmbed : ContainerNode<ChatEmbed>
    {
        #region Properties
        /// <summary>
        /// The list of embeds in this embed node.
        /// </summary>
        /// <value>List of embeds?</value>
        [JsonIgnore]
        public IList<Embed> Embeds
        {
            get => Data.Embeds;
            set => Data.Embeds = value;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// An element that contains embeds.
        /// </summary>
        /// <param name="embeds">The list of embeds this node has</param>
        public ChatEmbed(IList<Embed> embeds) : base(NodeType.WebhookMessage, ElementType.Block) =>
            Data.Embeds = embeds;
        /// <summary>
        /// An element that contains embeds.
        /// </summary>
        /// <param name="embeds">The array of embeds this node has</param>
        public ChatEmbed(params Embed[] embeds) : this(embeds.ToList()) { }
        #endregion

        #region Additional
        /// <summary>
        /// Adds a given embed to the embed list.
        /// </summary>
        /// <param name="embed">An embed to add to the list</param>
        /// <returns>This</returns>
        public ChatEmbed AddEmbed(Embed embed)
        {
            Embeds.Add(embed);
            return this;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns the count of all embeds in this node.
        /// </summary>
        /// <returns>Embed as string</returns>
        public override string ToString() => $"[Embed count: {Embeds.Count}]\n";
        #endregion
    }
}