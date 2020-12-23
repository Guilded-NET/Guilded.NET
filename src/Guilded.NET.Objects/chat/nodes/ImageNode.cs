using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents chat images in Guilded.
    /// </summary>
    public class ImageNode: ContainerNode<IMessageObject> {
        /// <summary>
        /// Represents chat images in Guilded.
        /// </summary>
        public ImageNode() =>
            (Type, Object) = (NodeType.Image, MsgObject.Block);
        /// <summary>
        /// Source URL of the image.
        /// </summary>
        /// <value>URL</value>
        [JsonIgnore]
        public Uri SourceURL {
            get {
                // Get source
                object src = GetDataProperty("src");
                // If source is null, return null
                if(src == null) return null;
                // To make sure it's string
                if(src is string str) return new Uri(str);
                // Or a uri
                else if(src is Uri uri) return uri;
                else return null;
            }
        }
        /// <summary>
        /// Turns image node to string.
        /// </summary>
        /// <returns>Image node as string</returns>
        public override string ToString() => $"[ Image {SourceURL} ]\n";
        /// <summary>
        /// Generates an image node.
        /// </summary>
        /// <param name="url">URL of the image</param>
        /// <returns>Image node</returns>
        public static ImageNode Generate(Uri url) =>
            new ImageNode {
                Data = new Dictionary<string, object> {
                    {"src", url.ToString()}
                },
                Nodes = new List<IMessageObject> {
                    TextObj.GenerateText("")
                }
            };
    }
}