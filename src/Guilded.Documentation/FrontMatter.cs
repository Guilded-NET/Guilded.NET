using DefaultDocumentation.Api;
using DefaultDocumentation.Models;
using DefaultDocumentation.Models.Members;
using DefaultDocumentation.Models.Types;

namespace Guilded.Documentation;

public sealed class FrontMatterStartSection : ISection
{
    public string Name => "FrontMatterStart";

    public void Write(IWriter writer)
    {
        DocItem item = writer.GetDocItem();
        string itemType = item switch
        {
            NamespaceDocItem => "namespace",
            AssemblyDocItem => "assembly",
            TypeDocItem type => type.Type.Kind.ToString().ToLower(),
            ConstructorDocItem => "constructor",
            FieldDocItem => "field",
            PropertyDocItem => "property",
            MethodDocItem => "method",
            EventDocItem => "event",
            OperatorDocItem => "operator",
            _ => "unknown"
        };
        writer.AppendLine("---")
            .Append("name: ")
            .AppendLine(item.Name)
            .Append("type: ")
            .AppendLine(itemType)
            .AppendLine("tags:")
            .AppendLine("  - references")
            .Append("  - ")
            .AppendLine(itemType)
            .Append("description: \"");
    }
}
public sealed class FrontMatterEndSection : ISection
{
    public string Name => "FrontMatterEnd";

    public void Write(IWriter writer) => writer.AppendLine("\"").AppendLine("---");
}