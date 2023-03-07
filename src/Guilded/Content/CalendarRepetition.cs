using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents a model for creating repeating <see cref="CalendarEvent">calendar events</see>.
/// </summary>
/// <seealso cref="CalendarEvent" />
/// <seealso cref="CalendarEventInterval" />
/// <seealso cref="CalendarEventRepetitionType" />
public class CalendarEventRepetition
{
    #region Properties
    /// <summary>
    /// Gets how often a <see cref="CalendarEvent">calendar event</see> repeats.
    /// </summary>
    /// <remarks>
    /// <para>This will repeat for the next 180 days unless <see cref="CalendarEventRepetitionType.Custom" /> is defined.</para>
    /// </remarks>
    /// <value>How often a <see cref="CalendarEvent">calendar event</see> repeats</value>
    /// <seealso cref="CalendarEventRepetition" />
    /// <seealso cref="Every" />
    /// <seealso cref="On" />
    public CalendarEventRepetitionType Type { get; }

    /// <summary>
    /// Gets the week days the <see cref="CalendarEvent">calendar event</see> repeats on.
    /// </summary>
    /// <value>The week days the <see cref="CalendarEvent">calendar event</see> repeats on</value>
    /// <seealso cref="CalendarEventRepetition" />
    /// <seealso cref="Type" />
    /// <seealso cref="Every" />
    public IList<DayOfWeek>? On { get; }

    /// <summary>
    /// Gets the interval between each repeated <see cref="CalendarEvent">calendar event</see>.
    /// </summary>
    /// <remarks>
    /// <para>This must have <see cref="Type" /> set to <see cref="CalendarEventRepetitionType.Custom" />.</para>
    /// </remarks>
    /// <value>The interval between each repeated <see cref="CalendarEvent">calendar event</see></value>
    /// <seealso cref="CalendarEventRepetition" />
    /// <seealso cref="Type" />
    /// <seealso cref="On" />
    public CalendarEventInterval? Every { get; }
    #endregion

    #region Properties End
    /// <summary>
    /// Gets the date that the <see cref="CalendarEventRepetition">calendar event repetition</see> ends at.
    /// </summary>
    /// <remarks>
    /// <para>Used to control the end date of the <see cref="CalendarEventRepetition">calendar event repetition</see> (only used when <see cref="Type">type</see> is <see cref="CalendarEventRepetitionType.Custom"><c>custom</c></see>; if used with <see cref="EndsAfterOccurrences" />, the earliest resultant date of the two will be used).</para>
    /// </remarks>
    /// <value>The date that the <see cref="CalendarEventRepetition">calendar event repetition</see> ends at</value>
    /// <seealso cref="CalendarEventRepetition" />
    /// <seealso cref="EndsAfterOccurrences" />
    public DateTime? EndDate { get; }

    /// <summary>
    /// Gets the count after which the <see cref="CalendarEventRepetition">calendar event repetition</see> ends.
    /// </summary>
    /// <remarks>
    /// <para>Used to control the end date of the <see cref="CalendarEventRepetition">calendar event repetition</see> (only used when <see cref="Type">type</see> is <see cref="CalendarEventRepetitionType.Custom"><c>custom</c></see>; if used with <see cref="EndsAfterOccurrences" />, the earliest resultant date of the two will be used).</para>
    /// </remarks>
    /// <value>The count after which the <see cref="CalendarEventRepetition">calendar event repetition</see> ends</value>
    /// <seealso cref="CalendarEventRepetition" />
    /// <seealso cref="EndDate" />
    public uint? EndsAfterOccurrences { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CalendarEventRepetition" /> from the specified JSON properties.
    /// </summary>
    /// <param name="type">How often a <see cref="CalendarEvent">calendar event</see> repeats</param>
    /// <param name="on">The week days the <see cref="CalendarEvent">calendar event</see> repeats on</param>
    /// <param name="every">The interval between each repeated <see cref="CalendarEvent">calendar event</see></param>
    /// <param name="endDate">The date that the <see cref="CalendarEventRepetition">calendar event repetition</see> ends at. Used to control the end date of the <see cref="CalendarEventRepetition">event repetition</see> (only used when <see cref="Type">type</see> is <see cref="CalendarEventRepetitionType.Custom"><c>custom</c></see>; if used with <see cref="EndsAfterOccurrences" />, the earliest resultant date of the two will be used)</param>
    /// <param name="endsAfterOccurrences">The count after which the <see cref="CalendarEventRepetition">calendar event repetition</see> ends. Used to control the end date of the <see cref="CalendarEventRepetition">event repetition</see> (only used when <see cref="Type">type</see> is <see cref="CalendarEventRepetitionType.Custom"><c>custom</c></see>; if used with <see cref="EndsAfterOccurrences" />, the earliest resultant date of the two will be used)</param>
    /// <returns>New <see cref="CalendarEventRepetition" /> JSON instance</returns>
    /// <seealso cref="CalendarEventRepetition" />
    [JsonConstructor]
    public CalendarEventRepetition(
        [JsonProperty(Required = Required.Always)]
        CalendarEventRepetitionType type,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<DayOfWeek>? on = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        CalendarEventInterval? every = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? endDate = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? endsAfterOccurrences = null
    ) =>
        (Type, On, Every, EndDate, EndsAfterOccurrences) = (type, on, every, endDate, endsAfterOccurrences);
    #endregion
}

/// <summary>
/// Represents an interval between each <see cref="CalendarEventRepetition">calendar event repetition</see>.
/// </summary>
/// <seealso cref="CalendarEvent" />
/// <seealso cref="CalendarEventRepetition" />
/// <seealso cref="CalendarEventIntervalType" />
public class CalendarEventInterval
{
    #region Properties
    /// <summary>
    /// Gets on what the <see cref="CalendarEvent">calendar event</see> should repeat.
    /// </summary>
    /// <example>
    /// <para>If the value is <see cref="CalendarEventIntervalType.Day">day</see> <see cref="Count" /> is <c>2</c> , then it will repeat on every second day.</para>
    /// </example>
    /// <value>On which every <see cref="Interval">interval</see> the <see cref="CalendarEvent">calendar event</see> should repeat on</value>
    /// <seealso cref="CalendarEventInterval" />
    /// <seealso cref="CalendarEventRepetition" />
    /// <seealso cref="Interval" />
    public CalendarEventIntervalType Interval { get; }

    /// <summary>
    /// Gets on which every <see cref="Interval">interval</see> the <see cref="CalendarEvent">calendar event</see> should repeat on.
    /// </summary>
    /// <example>
    /// <para>If the value is <c>1</c>, then the <see cref="CalendarEvent">calendar event</see> will be on every <see cref="Interval">interval</see>.</para>
    /// <para>If the value is <c>2</c>, then the <see cref="CalendarEvent">calendar event</see> will be on every second <see cref="Interval">interval</see>.</para>
    /// </example>
    /// <value>On which every <see cref="Interval">interval</see> the <see cref="CalendarEvent">calendar event</see> should repeat on</value>
    /// <seealso cref="CalendarEventInterval" />
    /// <seealso cref="CalendarEventRepetition" />
    /// <seealso cref="Interval" />
    public uint Count { get; }
    #endregion
}

/// <summary>
/// How often the <see cref="CalendarEvent">calendar event</see> happens.
/// </summary>
/// <remarks>
/// <para>This is similar to <see cref="CalendarEventIntervalType" />, but with less customizability.</para>
/// </remarks>
/// <seealso cref="CalendarEventRepetition" />
/// <seealso cref="CalendarEventIntervalType" />
public enum CalendarEventRepetitionType
{
    /// <summary>
    /// The <see cref="CalendarEvent">calendar event</see> doesn't repeat.
    /// </summary>
    /// <seealso cref="EveryDay" />
    /// <seealso cref="EveryWeek" />
    /// <seealso cref="EveryMonth" />
    /// <seealso cref="Custom" />
    Once,

    /// <summary>
    /// The <see cref="CalendarEvent">calendar event</see> repeats every single day.
    /// </summary>
    /// <seealso cref="Once" />
    /// <seealso cref="EveryWeek" />
    /// <seealso cref="EveryMonth" />
    /// <seealso cref="Custom" />
    EveryDay,

    /// <summary>
    /// The <see cref="CalendarEvent">calendar event</see> repeats every single week.
    /// </summary>
    /// <seealso cref="Once" />
    /// <seealso cref="EveryDay" />
    /// <seealso cref="EveryMonth" />
    /// <seealso cref="Custom" />
    EveryWeek,

    /// <summary>
    /// The <see cref="CalendarEvent">calendar event</see> repeats every single month.
    /// </summary>
    /// <seealso cref="Once" />
    /// <seealso cref="EveryDay" />
    /// <seealso cref="EveryWeek" />
    /// <seealso cref="Custom" />
    EveryMonth,

    /// <summary>
    /// The <see cref="CalendarEvent">calendar event</see> can repeat at any given time provided by other <see cref="CalendarEventRepetition">calendar event repetition's</see> properties. 
    /// </summary>
    /// <seealso cref="Once" />
    /// <seealso cref="EveryDay" />
    /// <seealso cref="EveryWeek" />
    /// <seealso cref="EveryMonth" />
    Custom
}

/// <summary>
/// What is the interval between each <see cref="CalendarEvent">calendar event</see>.
/// </summary>
/// <remarks>
/// <para>This is similar to <see cref="CalendarEventRepetitionType" />, but used for more customizability.</para>
/// </remarks>
/// <seealso cref="CalendarEventInterval" />
public enum CalendarEventIntervalType
{
    /// <summary>
    /// The <see cref="CalendarEvent">calendar event</see> should repeat on every <c>x</c> day.
    /// </summary>
    /// <seealso cref="Week" />
    /// <seealso cref="Month" />
    /// <seealso cref="Year" />
    Day,

    /// <summary>
    /// The <see cref="CalendarEvent">calendar event</see> should repeat on every <c>x</c> week.
    /// </summary>
    /// <seealso cref="Day" />
    /// <seealso cref="Month" />
    /// <seealso cref="Year" />
    Week,

    /// <summary>
    /// The <see cref="CalendarEvent">calendar event</see> should repeat on every <c>x</c> month.
    /// </summary>
    /// <seealso cref="Day" />
    /// <seealso cref="Week" />
    /// <seealso cref="Year" />
    Month,

    /// <summary>
    /// The <see cref="CalendarEvent">calendar event</see> should repeat on every <c>x</c> year.
    /// </summary>
    /// <seealso cref="Day" />
    /// <seealso cref="Week" />
    /// <seealso cref="Month" />
    Year
}