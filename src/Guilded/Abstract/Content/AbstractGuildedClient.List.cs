using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Content;
using Guilded.Events;
using Guilded.Permissions;
using Guilded.Servers;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Properties List channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Item">list item</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ItemUpdated" />
    /// <seealso cref="ItemDeleted" />
    /// <seealso cref="ItemCompleted" />
    /// <seealso cref="ItemUncompleted" />
    public IObservable<ItemEvent> ItemCreated => ((IEventInfo<ItemEvent>)GuildedEvents["ListItemCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Item">list item</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ItemCreated" />
    /// <seealso cref="ItemDeleted" />
    /// <seealso cref="ItemCompleted" />
    /// <seealso cref="ItemUncompleted" />
    public IObservable<ItemEvent> ItemUpdated => ((IEventInfo<ItemEvent>)GuildedEvents["ListItemUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Item">list item</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ItemCreated" />
    /// <seealso cref="ItemUpdated" />
    /// <seealso cref="ItemCompleted" />
    /// <seealso cref="ItemUncompleted" />
    public IObservable<ItemEvent> ItemDeleted => ((IEventInfo<ItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Item">list item</see> is set as completed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemCompleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ItemUpdated" />
    /// <seealso cref="ItemUpdated" />
    /// <seealso cref="ItemCompleted" />
    /// <seealso cref="ItemUncompleted" />
    public IObservable<ItemEvent> ItemCompleted => ((IEventInfo<ItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Item">list item</see> is set as not completed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ListItemUncompleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ItemUpdated" />
    /// <seealso cref="ItemDeleted" />
    /// <seealso cref="ItemCompleted" />
    /// <seealso cref="ItemUncompleted" />
    public IObservable<ItemEvent> ItemUncompleted => ((IEventInfo<ItemEvent>)GuildedEvents["ListItemDeleted"]).Observable;
    #endregion

    #region Methods List channels
    /// <summary>
    /// Gets a set of <see cref="Item">list items</see> from the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to get list items from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <returns>The list of fetched <see cref="Item">list items</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<ItemSummary>> GetItemsAsync(Guid channel) =>
        GetResponsePropertyAsync<IList<ItemSummary>>(new RestRequest($"channels/{channel}/items", Method.Get), "listItems");

    /// <summary>
    /// Gets the specified <paramref name="listItem">list item</paramref> from a <paramref name="channel">list channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <returns>The <see cref="Item">list item</see> that was specified in the arguments</returns>
    public Task<Item> GetItemAsync(Guid channel, Guid listItem) =>
        GetResponsePropertyAsync<Item>(new RestRequest($"channels/{channel}/items/{listItem}", Method.Get), "listItem");

    /// <summary>
    /// Creates a new <see cref="Item">list item</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The text content of the <see cref="Item">list item</see></param>
    /// <param name="note">The text content of an <see cref="ItemNote">optional note</see> in the <see cref="Item">list item</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <permission cref="Permission.CreateItems" />
    /// <permission cref="Permission.UseEveryoneMention">Required when posting a <see cref="Item">list item</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Item">list item</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Item> CreateItemAsync(Guid channel, string message, string? note = null) =>
        string.IsNullOrWhiteSpace(message)
        ? throw new ArgumentNullException(nameof(message))
        : GetResponsePropertyAsync<Item>(new RestRequest($"channels/{channel}/items", Method.Post)
            .AddJsonBody(new
            {
                message,
                note = new
                {
                    content = note
                }
            })
        , "listItem");

    /// <summary>
    /// Edits the <paramref name="message">text contents</paramref> of the specified <paramref name="listItem">list item</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to edit</param>
    /// <param name="message">The new text content of the <see cref="Item">list item</see></param>
    /// <param name="note">The new text content of the note in the <see cref="Item">list item</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <permission cref="Permission.UpdateItems">Required when updating <see cref="Item">list items</see> the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to a <see cref="Item">list item</see></permission>
    /// <returns>The <paramref name="listItem">list item</paramref> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Item> UpdateItemAsync(Guid channel, Guid listItem, string? message = null, string? note = null)
    {
        if (message is null && note is null)
            throw new ArgumentNullException(nameof(message), "Either the message or the note of the list item's update must be specified");
        else if (message is not null && string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(nameof(message), $"{nameof(message)} cannot be an empty or whitespace-only. Set it to null if you don't want to update the message of a list item.");
        else if (note is not null && string.IsNullOrWhiteSpace(note))
            throw new ArgumentNullException(nameof(note), $"{nameof(note)} cannot be an empty or whitespace-only. Set it to null if you don't want to update the note of a list item.");

        return GetResponsePropertyAsync<Item>(new RestRequest($"channels/{channel}/items/{listItem}", Method.Patch)
            .AddJsonBody(new
            {
                message,
                note = note is not null ? new
                {
                    content = note
                } : null
            })
        , "listItem");
    }

    /// <summary>
    /// Deletes the specified <paramref name="listItem">list item</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Item">list item</see> is</param>
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <permission cref="Permission.DeleteItems">Required when deleting <see cref="Item">list items</see> you don't own</permission>
    public Task DeleteItemAsync(Guid channel, Guid listItem) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}", Method.Delete));

    /// <summary>
    /// Marks the specified <paramref name="listItem">list item</paramref> as <see cref="ItemBase{T}.IsCompleted">completed</see>.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Item">list item</see> is</param>
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to complete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <permission cref="Permission.CompleteItems" />
    public Task CompleteItemAsync(Guid channel, Guid listItem) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}/complete", Method.Post));

    /// <summary>
    /// Marks the specified <paramref name="listItem">list item</paramref> as <see cref="ItemBase{T}.IsCompleted">not completed</see>.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> where the list item is</param>
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to complete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetItems" />
    /// <permission cref="Permission.CompleteItems" />
    public Task UncompleteItemAsync(Guid channel, Guid listItem) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/items/{listItem}/complete", Method.Delete));
    #endregion
}