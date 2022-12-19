namespace Case.Net.Emit.Delimiters;

public sealed class SingleCharDelimiterEmitter : IDelimiterEmitter
{
    private readonly string _strDelimiter;

    public char Delimiter { get; }
    public bool CheckWordEndStart { get; }

    public SingleCharDelimiterEmitter(char delimiter, bool checkWordEndStart = true)
    {
        Delimiter         = delimiter;
        _strDelimiter     = delimiter.ToString();
        CheckWordEndStart = checkWordEndStart;
    }

    public string EmitDelimiter(IReadOnlyList<string> words, int index)
    {
        var current = words[index];
        var next    = words[index + 1];

        if ( CheckWordEndStart )
        {
            if ( current[^1] == Delimiter )
                return string.Empty;

            if ( next[0] == Delimiter )
                return string.Empty;
        }

        return _strDelimiter;
    }
}
