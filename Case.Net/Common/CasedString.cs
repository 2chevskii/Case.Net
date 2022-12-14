using Case.Net.Common.Conventions;

namespace Case.Net.Common;

public readonly struct CasedString
{
    public string Input { get; }
    public IReadOnlyList<Word> Words { get; }
    public NamingConvention Convention { get; }
}
