using System.Collections.Generic;
using Guilded.Base.Content;

namespace Guilded.Markdown;

/// <summary>
/// Provides multiple dictionaries related to <see cref="Emote">emotes</see>.
/// </summary>
/// <seealso cref="Emote" />
public static class Emotes
{
    /// <summary>
    /// Gets the dictionary of <see cref="Emote">emote</see> symbols to their <see cref="Emote.Id">IDs</see>.
    /// </summary>
    /// <returns>The dictionary of <see cref="Emote">emote</see> symbols to their <see cref="Emote.Id">IDs</see></returns>
    /// <seealso cref="ByName" />
    public static IDictionary<char, uint> BySymbol { get; } = new Dictionary<char, uint>()
    {
        { 'a', 10 }
    };

    /// <summary>
    /// Gets the dictionary of <see cref="Emote">emote</see> <see cref="Emote.Name">names</see> to their <see cref="Emote.Id">IDs</see>.
    /// </summary>
    /// <returns>The dictionary of <see cref="Emote">emote</see> <see cref="Emote.Name">names</see> to their <see cref="Emote.Id">IDs</see></returns>
    /// <seealso cref="BySymbol" />
    public static IDictionary<string, uint> ByName { get; } = new Dictionary<string, uint>()
    {
        { "a", 10 }
    };
}