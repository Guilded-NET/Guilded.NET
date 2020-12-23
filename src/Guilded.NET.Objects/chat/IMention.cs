namespace Guilded.NET.Objects.Chat {
    public interface IMention {
        string Matcher {
            get; set;
        }
        string Name {
            get; set;
        }
    }
}