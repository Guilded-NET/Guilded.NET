using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Guilded.Base.Json;
using Newtonsoft.Json;

namespace Guilded.Base;

/// <summary>
/// Represents an identifier for Guilded servers, users, etc.
/// </summary>
/// <remarks>
/// <para>Guilded hash identifier is 8 characters or more in length and consists of digits, uppercase letters and lowercase letters.</para>
/// <para>This can be found in:</para>
/// <list type="bullet">
///     <item>
///         <description>Users, members, friends</description>
///     </item>
///     <item>
///         <description>Servers/teams</description>
///     </item>
///     <item>
///         <description>Groups, tournaments</description>
///     </item>
///     <item>
///         <description>Invites, invite links</description>
///     </item>
///     <item>
///         <description>Announcements</description>
///     </item>
/// </list>
/// </remarks>
/// <example>
/// <para>The list of random Guilded hash identifiers:</para>
/// <code language="none">
/// R40Mp0Wd
/// Ann6LewA
/// </code>
/// </example>
/// <seealso cref="Guid" />
/// <seealso cref="FormId" />
[TypeConverter(typeof(HashIdConverter))]
[JsonConverter(typeof(IdConverter))]
public readonly struct HashId : IEquatable<HashId>
{
    #region Static fields & Constants
    private const int idMinLength = 8;

    private const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    #endregion

    #region Fields
    internal readonly string _;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="HashId" />.
    /// </summary>
    /// <param name="id">The raw string in the format of Guilded Hash ID</param>
    /// <exception cref="FormatException">When the given ID string is in incorrect format</exception>
    public HashId(string id)
    {
        // Make sure it's in correct format
        if (!Check(id))
            throw new FormatException("The given hash ID string is not in correct format");

        _ = id;
    }

    private HashId(ref string id) =>
        _ = id;
    #endregion

    #region Methods
    /// <summary>
    /// Returns the <see cref="string" /> representation of <see cref="HashId">hash ID</see> instance.
    /// </summary>
    /// <remarks>
    /// <para>The raw string that makes up <see cref="HashId" /> will be returned.</para>
    /// </remarks>
    /// <returns>The <see cref="string" /> representation of <see cref="HashId">hash ID</see> instance</returns>
    public override string ToString() =>
        _;

    /// <summary>
    /// Gets the hashcode of the <see cref="HashId">hash ID</see>.
    /// </summary>
    /// <returns>The hashcode of the <see cref="HashId">hash ID</see></returns>
    public override int GetHashCode() =>
        _.GetHashCode();

    /// <summary>
    /// Returns whether this <see cref="HashId" /> instance and the <paramref name="other" /> given value are equal.
    /// </summary>
    /// <param name="other">Another identifier to compare</param>
    /// <returns>Both are equal</returns>
    public bool Equals(HashId other) =>
        Equals(other._);

    /// <summary>
    /// Returns whether this <see cref="HashId" /> instance and the <paramref name="other" /> given value are equal.
    /// </summary>
    /// <param name="other">Another identifier to compare</param>
    /// <returns>Both are equal</returns>
    public bool Equals(string other) =>
        other == _;

    /// <summary>
    /// Returns whether this <see cref="HashId" /> instance and the <paramref name="other" /> given value are equal.
    /// </summary>
    /// <param name="other">Another object to compare</param>
    /// <returns>Both are equal</returns>
    public override bool Equals(object? other) =>
        other is not null && other is HashId id && Equals(id);
    #endregion

    #region Operators
    /// <summary>
    /// Returns whether <paramref name="id0" /> and <paramref name="id1" /> are equal.
    /// </summary>
    /// <param name="id0">First ID to be compared</param>
    /// <param name="id1">Second ID to be compared</param>
    /// <returns>Both are equal</returns>
    public static bool operator ==(string id0, HashId id1) =>
        id0 == id1._;

    /// <summary>
    /// Returns whether <paramref name="id0" /> and <paramref name="id1" /> are equal.
    /// </summary>
    /// <param name="id0">First ID to be compared</param>
    /// <param name="id1">Second ID to be compared</param>
    /// <returns>Both are equal</returns>
    public static bool operator ==(HashId id0, string id1) =>
        id0._ == id1;

    /// <summary>
    /// Returns whether <paramref name="id0" /> and <paramref name="id1" /> are equal.
    /// </summary>
    /// <param name="id0">First ID to be compared</param>
    /// <param name="id1">Second ID to be compared</param>
    /// <returns>Both are equal</returns>
    public static bool operator ==(HashId id0, HashId id1) =>
        id0._ == id1._;

    /// <summary>
    /// Returns whether <paramref name="id0" /> and <paramref name="id1" /> are not equal.
    /// </summary>
    /// <param name="id0">First ID to be compared</param>
    /// <param name="id1">Second ID to be compared</param>
    /// <returns>Both aren't equal</returns>
    public static bool operator !=(HashId id0, HashId id1) =>
        !(id0 == id1);

    /// <summary>
    /// Returns whether <paramref name="id0" /> and <paramref name="id1" /> are not equal.
    /// </summary>
    /// <param name="id0">First ID to be compared</param>
    /// <param name="id1">Second ID to be compared</param>
    /// <returns>Both aren't equal</returns>
    public static bool operator !=(string id0, HashId id1) =>
        !(id0 == id1);

    /// <summary>
    /// Returns whether <paramref name="id0" /> and <paramref name="id1" /> are not equal.
    /// </summary>
    /// <param name="id0">First ID to be compared</param>
    /// <param name="id1">Second ID to be compared</param>
    /// <returns>Both aren't equal</returns>
    public static bool operator !=(HashId id0, string id1) =>
        !(id0 == id1);
    #endregion

    #region Static methods
    /// <summary>
    /// Returns whether <paramref name="str" /> is in the correct <see cref="HashId" /> format.
    /// </summary>
    /// <param name="str">A raw string to check</param>
    /// <returns>Correct formatting</returns>
    public static bool Check(string? str) =>
        str is not null && str.Length >= idMinLength && str.All(ch => allowedChars.Contains(ch));

    /// <summary>
    /// Converts <see cref="string" /> to a <see cref="HashId" /> and returns whether the it was correctly converted.
    /// </summary>
    /// <param name="input">The <see cref="string" /> to convert to <see cref="HashId" /></param>
    /// <param name="value">The converted value</param>
    /// <returns>Whether the <paramref name="value" /> was parsed</returns>
    public static bool TryParse([NotNullWhen(true)] string? input, out HashId value)
    {
        // Bad
        value = default;
        if (!Check(input)) return false;

        // Good
        value = new HashId(ref input!);
        return true;
    }
    #endregion
}