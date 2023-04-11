using Guilded.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Guilded.Content;

/// <summary>
/// Represents the status of <see cref="CalendarEventRsvp">event RSVP</see>.
/// </summary>
/// <seealso cref="CalendarEventRsvp" />
/// <seealso cref="CalendarEvent" />
/// <seealso cref="MessageType" />
[JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
public enum CalendarEventRsvpStatus
{
    /// <summary>
    /// The <see cref="User">user</see> will attend the <see cref="CalendarEvent">event</see>.
    /// </summary>
    /// <seealso cref="CalendarEventRsvpStatus" />
    /// <seealso cref="Maybe" />
    /// <seealso cref="Declined" />
    /// <seealso cref="Invited" />
    /// <seealso cref="Waitlisted" />
    Going,

    /// <summary>
    /// The <see cref="User">user</see> has not decided if they will attend the <see cref="CalendarEvent">event</see>.
    /// </summary>
    /// <seealso cref="CalendarEventRsvpStatus" />
    /// <seealso cref="Going" />
    /// <seealso cref="Declined" />
    /// <seealso cref="Invited" />
    /// <seealso cref="Waitlisted" />
    Maybe,

    /// <summary>
    /// The <see cref="User">user</see> will not attend the <see cref="CalendarEvent">event</see>.
    /// </summary>
    /// <seealso cref="CalendarEventRsvpStatus" />
    /// <seealso cref="Going" />
    /// <seealso cref="Maybe" />
    /// <seealso cref="Invited" />
    /// <seealso cref="Waitlisted" />
    Declined,

    /// <summary>
    /// The <see cref="User">user</see> has been invited to attend the <see cref="CalendarEvent">event</see> by <see cref="CalendarEventRsvp.CreatedBy">someone else</see>.
    /// </summary>
    /// <seealso cref="CalendarEventRsvpStatus" />
    /// <seealso cref="Going" />
    /// <seealso cref="Maybe" />
    /// <seealso cref="Declined" />
    /// <seealso cref="Waitlisted" />
    Invited,

    /// <summary>
    /// The <see cref="User">user</see> is asked for a response on attending the <see cref="CalendarEvent">event</see>.
    /// </summary>
    /// <seealso cref="CalendarEventRsvpStatus" />
    /// <seealso cref="Going" />
    /// <seealso cref="Maybe" />
    /// <seealso cref="Declined" />
    /// <seealso cref="Invited" />
    Waitlisted
}