using System;
using System.Diagnostics.CodeAnalysis;
using Guilded.Base;
using Guilded.Servers;
using Guilded.Users;

namespace Guilded;

/// <summary>
/// Represents the <see cref="IUserCreated">content</see> that can be created by a <see cref="User">user</see>.
/// </summary>
public interface IUserCreated
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that created the content.
    /// </summary>
    /// <remarks>
    /// <para>If a <see cref="Webhook">webhook</see> created the <see cref="IUserCreated">content</see>, the value of this property will be <c>Ann6LewA</c>.</para>
    /// </remarks>
    /// <value>The identifier of <see cref="User">user</see> that created the content</value>
    /// <seealso cref="IUserCreated" />
    /// <seealso cref="ICreatableContent" />
    /// <seealso cref="IWebhookCreated.CreatedByWebhook" />
    /// <seealso cref="ICreationDated.CreatedAt" />
    /// <seealso cref="IUpdatableContent.UpdatedAt" />
    HashId CreatedBy { get; }
    #endregion
}

/// <summary>
/// Represents the <see cref="ICreationDated">content</see> that can be created and has specified <see cref="CreatedAt">creation date</see>.
/// </summary>
/// <seealso cref="IUserCreated" />
/// <seealso cref="IUpdatableContent" />
/// <seealso cref="IWebhookCreated" />
/// <seealso cref="Content.IReactibleContent" />
public interface ICreationDated
{
    #region Properties
    /// <summary>
    /// Gets the date when the <see cref="ICreationDated">content</see> was created.
    /// </summary>
    /// <value>The date when the <see cref="ICreationDated">content</see> was created</value>
    /// <seealso cref="ICreationDated" />
    /// <seealso cref="IUserCreated" />
    /// <seealso cref="IUserCreated.CreatedBy" />
    /// <seealso cref="IUpdatableContent.UpdatedAt" />
    /// <seealso cref="IWebhookCreated.CreatedByWebhook" />
    DateTime CreatedAt { get; }
    #endregion
}

/// <summary>
/// Represents the <see cref="ICreatableContent">content</see> that can be created and has specified <see cref="ICreationDated.CreatedAt">creation date</see> and <see cref="IUserCreated.CreatedBy">user creator</see>.
/// </summary>
/// <seealso cref="IUserCreated" />
/// <seealso cref="ICreationDated" />
/// <seealso cref="IUpdatableContent" />
/// <seealso cref="IWebhookCreated" />
/// <seealso cref="Content.IReactibleContent" />
public interface ICreatableContent : IUserCreated, ICreationDated { }

/// <summary>
/// Represents the content that can be edited.
/// </summary>
/// <seealso cref="ICreatableContent" />
/// <seealso cref="IUserCreated" />
/// <seealso cref="IWebhookCreated" />
/// <seealso cref="Content.IReactibleContent" />
public interface IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets the date when the <see cref="IUpdatableContent">content</see> were edited.
    /// </summary>
    /// <remarks>
    /// <para>Only returns the most recent update.</para>
    /// </remarks>
    /// <value>The date when the <see cref="IUpdatableContent">content</see> were edited</value>
    /// <seealso cref="IUpdatableContent" />
    /// <seealso cref="ICreationDated.CreatedAt" />
    /// <seealso cref="IUserCreated.CreatedBy" />
    /// <seealso cref="IWebhookCreated.CreatedByWebhook" />
    DateTime? UpdatedAt { get; }
    #endregion
}

/// <summary>
/// Represents the content that can be created by a <see cref="Webhook">webhook</see>.
/// </summary>
/// <seealso cref="IUserCreated" />
/// <seealso cref="ICreatableContent" />
/// <seealso cref="IUpdatableContent" />
/// <seealso cref="Content.IReactibleContent" />
public interface IWebhookCreated
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Webhook">webhook</see> that created the content.
    /// </summary>
    /// <value>The identifier of the <see cref="Webhook">webhook</see> that created the content</value>
    /// <seealso cref="IWebhookCreated" />
    /// <seealso cref="IUserCreated.CreatedBy" />
    /// <seealso cref="ICreationDated.CreatedAt" />
    /// <seealso cref="IUpdatableContent.UpdatedAt" />
    Guid? CreatedByWebhook { get; }
    #endregion
}

/// <summary>
/// Represents the content that can be archived.
/// </summary>
/// <seealso cref="IUserCreated" />
/// <seealso cref="ICreatableContent" />
/// <seealso cref="IUpdatableContent" />
/// <seealso cref="Content.IReactibleContent" />
public interface IArchivableContent
{
    /// <summary>
    /// Gets the date when the <see cref="IArchivableContent">content</see> was archived.
    /// </summary>
    /// <value>The date when the <see cref="IArchivableContent">content</see> was archived</value>
    /// <seealso cref="IArchivableContent" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="IsArchived" />
    DateTime? ArchivedAt { get; }

    /// <summary>
    /// Gets the <see cref="User">user</see> who archived the <see cref="IArchivableContent">content</see>.
    /// </summary>
    /// <value>The <see cref="User">user</see> who archived the <see cref="IArchivableContent">content</see></value>
    /// <seealso cref="IArchivableContent" />
    /// <seealso cref="ArchivedAt" />
    /// <seealso cref="IsArchived" />
    HashId? ArchivedBy { get; }

    /// <summary>
    /// Gets whether the <see cref="IArchivableContent">content</see> has been archived.
    /// </summary>
    /// <value>Whether the <see cref="IArchivableContent">content</see> has been archived</value>
    /// <seealso cref="IArchivableContent" />
    /// <seealso cref="ArchivedAt" />
    /// <seealso cref="ArchivedBy" />
    [MemberNotNullWhen(true, nameof(ArchivedAt), nameof(ArchivedBy))]
    public bool IsArchived => ArchivedAt is not null;
}
