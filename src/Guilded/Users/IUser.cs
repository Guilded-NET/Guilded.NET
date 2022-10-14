using Guilded.Base;

namespace Guilded.Users;

/// <summary>
/// Represents the base interface for the <see cref="User">user</see> models.
/// </summary>
public interface IUser : IModelHasId<HashId>
{
    /// <summary>
    /// Gets the global username of the <see cref="User">user</see>.
    /// </summary>
    /// <value>Name</value>
    /// <seealso cref="IUser" />
    /// <seealso cref="User" />
    /// <seealso cref="UserSummary" />
    /// <seealso cref="UserSummary.Avatar" />
    /// <seealso cref="User.Banner" />
    public string Name { get; }
}
