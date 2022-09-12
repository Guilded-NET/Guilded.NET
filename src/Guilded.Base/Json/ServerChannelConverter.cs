using System;
using System.Drawing;
using Guilded.Base.Servers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.Base.Json;

/// <summary>
/// Converts <see cref="ServerChannel" /> to their types.
/// </summary>
public class ServerChannelConverter : JsonConverter
{
    #region Methods
    /// <summary>
    /// Gets whether the <see cref="ServerChannel" /> can be written into JSON using the <see cref="ServerChannelConverter">converter</see>.
    /// </summary>
    /// <value>Always <see langword="false" /></value>
    public override bool CanWrite => false;

    /// <summary>
    /// Writes given object as JSON.
    /// </summary>
    /// <param name="writer">The writer to use to write to JSON</param>
    /// <param name="value">The object to write to JSON</param>
    /// <param name="serializer">The serializer that is serializing the object</param>
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) =>
        writer.WriteNull();

    /// <summary>
    /// Reads the given JSON object as any of <see cref="ServerChannel" /> sub-types.
    /// </summary>
    /// <param name="reader">The reader that was used to read JSON</param>
    /// <param name="objectType">The type of the object to convert</param>
    /// <param name="existingValue">The previous value of the property being converted</param>
    /// <param name="serializer">The serializer that is deserializing the object</param>
    /// <returns><see cref="Color" /></returns>
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        ChannelType channelType = token.Value<ChannelType>("type");

        return channelType switch
        {
            ChannelType.Chat => serializer.Deserialize<ChatChannel>(reader),
            ChannelType.Forums => serializer.Deserialize<ForumChannel>(reader),
            ChannelType.Calendar => serializer.Deserialize<CalendarChannel>(reader),
            ChannelType.List => serializer.Deserialize<ListChannel>(reader),
            ChannelType.Voice => serializer.Deserialize<VoiceChannel>(reader),
            ChannelType.Stream => serializer.Deserialize<StreamChannel>(reader),
            ChannelType.Media => serializer.Deserialize<MediaChannel>(reader),
            ChannelType.Scheduling => serializer.Deserialize<SchedulingChannel>(reader),
            ChannelType.Announcements => serializer.Deserialize<AnnouncementChannel>(reader),
            _ => serializer.Deserialize<DocChannel>(reader)
        };
    }

    /// <summary>
    /// Returns whether the converter supports converting the given type.
    /// </summary>
    /// <param name="objectType">The type of object that potentially can be converted</param>
    /// <returns>Type can be converted</returns>
    public override bool CanConvert(Type objectType) =>
        objectType == typeof(ServerChannel);
    #endregion
}