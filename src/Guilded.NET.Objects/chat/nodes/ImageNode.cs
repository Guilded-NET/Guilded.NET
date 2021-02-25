using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Represents chat images in Guilded.
    /// </summary>
    public class ImageNode : ContainerNode<IMessageObject>
    {
        /// <summary>
        /// Represents chat images in Guilded.
        /// </summary>
        public ImageNode() =>
            Type = NodeType.Image;
        /// <summary>
        /// Source URL of the image.
        /// </summary>
        /// <value>URL</value>
        [JsonIgnore]
        public Uri SourceURL
        {
            get => GetDataProperty<Uri>("src");
        }
        /// <summary>
        /// Turns image node to string.
        /// </summary>
        /// <returns>Image node as string</returns>
        public override string ToString() => $"[ Image {SourceURL} ]\n{base.ToString()}";
        /// <summary>
        /// Generates an image node.
        /// </summary>
        /// <param name="src">URL of the image</param>
        /// <param name="caption">Caption to add</param>
        /// <returns>Image node</returns>
        public static ImageNode Generate(Uri src, string caption = null) =>
            new ImageNode
            {
                Data = JObject.FromObject(new { src }),
                Nodes = new List<IMessageObject> {
                    // C# 8 casting
                    caption == null ? TextObj.GenerateText("") : (IMessageObject)ImageCaption.Generate(Leaf.Generate(caption))
                }
            };
    }
}