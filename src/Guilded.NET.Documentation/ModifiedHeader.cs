using DefaultDocumentation.Api;
using DefaultDocumentation.Models;
using DefaultDocumentation.Models.Types;

namespace Guilded.NET.Documentation;

public sealed class ModifiedHeaderSection : ISection
{
    public string Name => "ModifiedHeader";

    public void Write(IWriter writer)
    {
        DocItem item = writer.GetDocItem();

        // Smaller text
        writer.AppendLine().Append("###### **Assembly:** ");

        AssemblyDocItem assembly = (AssemblyDocItem)item.GetParents().First();
        // Write a hyperlink if its page exists
        // if (assembly.HasOwnPage(writer.Context))
        //     writer.Append("[`")
        //           .Append(assembly.Name)
        //           .Append("`](")
        //           .Append(writer.Context.GetUrl(assembly))
        //           .Append(" '")
        //           .Append(assembly.Name)
        //           .Append("')");
        // else
        writer.Append("`")
              .Append(assembly.Name)
              .Append("`");

        DocItem? parentItem = item.Parent;
        // Only if namespace exists
        if (parentItem is not null)
        {
            writer.Append("<br/>**");
            writer.Append(parentItem is NamespaceDocItem ? "Namespace" : parentItem is TypeDocItem ? "Type" : "Parent");
            writer.Append(":** ");
            if (parentItem.HasOwnPage(writer.Context))
                writer.Append("[`")
                    .Append(parentItem.Name)
                    .Append("`](")
                    .Append(writer.Context.GetUrl(parentItem))
                    .Append(" '")
                    .Append(parentItem.FullName)
                    .Append("')");
            else
                writer.Append("`")
                        .Append(parentItem.Name)
                        .Append("`");
        }
        writer.AppendLine();
    }
}