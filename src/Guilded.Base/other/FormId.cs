using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.Base;

/// <summary>
/// An identifier for forms and media uploads.
/// </summary>
/// <remarks>
/// <para>The form identifier. This can only be found in forms(form fields specifically) and media upload progress tracking.</para>
/// </remarks>
/// <example>
/// <para>The list of different form identifiers:</para>
/// <code language="none">
/// r-1000000-1000000
/// r-2849201-1832734
/// r-7289920-2930323
/// r-4598392-4859302
/// </code>
/// </example>
/// <seealso cref="Guid"/>
/// <seealso cref="HashId"/>
[TypeConverter(typeof(FormIdConverter))]
[JsonConverter(typeof(IdConverter))]
public readonly struct FormId : IEquatable<FormId>
{
    internal readonly string _;
    private const int partLength = 7;
    private const string partChars = "0123456789";
    private static readonly Random random = new();
    /// <summary>
    /// Creates a random value of <see cref="FormId"/>.
    /// </summary>
    /// <value>New form ID</value>
    public static FormId Random => new($"r-{random.Next(1000000, 9999999)}-{random.Next(1000000, 9999999)}");
    /// <summary>
    /// The identifier for forms and media uploads.
    /// </summary>
    /// <param name="id">The raw string in the format of Form/Media ID</param>
    /// <exception cref="FormatException">When the given ID string is in incorrect format</exception>
    public FormId(string id)
    {
        // Make sure it's in correct format
        if (!Check(id))
            throw HashId.FormatError;

        _ = id;
    }

    #region Overrides
    /// <summary>
    /// Returns the string representation of this <see cref="FormId"/> instance.
    /// </summary>
    /// <returns><see cref="FormId"/> as string</returns>
    public override string ToString() =>
        _;
    /// <summary>
    /// Gets a hashcode of this object.
    /// </summary>
    /// <returns>HashCode</returns>
    public override int GetHashCode() =>
        HashCode.Combine(_, 1);
    /// <summary>
    /// Returns whether this <see cref="FormId"/> instance and <paramref name="other"/> are equal.
    /// </summary>
    /// <param name="other">Another identifier to compare</param>
    /// <returns>Both are equal</returns>
    public bool Equals(FormId other) =>
        other == this;
    /// <summary>
    /// Returns whether this <see cref="FormId"/> instance and <paramref name="obj"/> are equal.
    /// </summary>
    /// <param name="obj">Another object to compare</param>
    /// <returns>Both are equal</returns>
    public override bool Equals(object? obj) =>
        obj is FormId id && Equals(id);
    #endregion

    #region Operators
    /// <summary>
    /// Returns whether <paramref name="id0"/> and <paramref name="id1"/> are equal.
    /// </summary>
    /// <param name="id0">First ID to be compared</param>
    /// <param name="id1">Second ID to be compared</param>
    /// <returns>Both are equal</returns>
    public static bool operator ==(FormId id0, FormId id1) =>
        id0._ == id1._;
    /// <summary>
    /// Returns whether <paramref name="id0"/> and <paramref name="id1"/> are not equal.
    /// </summary>
    /// <param name="id0">First ID to be compared</param>
    /// <param name="id1">Second ID to be compared</param>
    /// <returns>Both aren't equal</returns>
    public static bool operator !=(FormId id0, FormId id1) =>
        !(id0 == id1);
    #endregion

    #region Static methods
    /// <summary>
    /// Checks if <paramref name="str"/> is in the correct <see cref="FormId"/> format.
    /// </summary>
    /// <param name="str">A raw string to check</param>
    /// <returns>Correct formatting</returns>
    public static bool Check(string str)
    {
        // Make sure it's in the format of r-1000000-1000000

        // (r)-1000000-1000000
        if (!str.StartsWith("r-") || string.IsNullOrWhiteSpace(str))
            return false;
        // Split by - and leave out 'r'
        // r-(1000000-1000000)
        List<string> split = str.Split('-').Skip(1).ToList();
        // r-(1000000)-(1000000)
        return split.Count == 2 && !split.Any(IsFormIdPart);
    }
    /// <summary>
    /// Checks if <paramref name="part"/> is in 7 digits.
    /// </summary>
    /// <param name="part">The part of the <see cref="FormId"/> to check</param>
    /// <returns><paramref name="part"/> is in correct format</returns>
    private static bool IsFormIdPart(string part) =>
        part.Length == partLength && part.All(ch => partChars.Contains(ch));
    #endregion
}