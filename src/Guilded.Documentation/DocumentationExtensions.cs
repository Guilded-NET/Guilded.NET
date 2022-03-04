using DefaultDocumentation.Api;
using DefaultDocumentation.Models;

namespace Guilded.Documentation;

internal static class DocumentationExtensions
{
    /// <summary>
    /// Gets the current item being documented.
    /// </summary>
    /// <param name="writer">The documentation writer</param>
    /// <returns>Doc item being written</returns>
    public static DocItem GetDocItem(this IWriter writer) =>
        writer["Markdown.CurrentItem"] as DocItem ?? writer.DocItem;
}