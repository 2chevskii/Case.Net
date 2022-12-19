namespace Case.Net.Emit.Suffixes;

public sealed class SingleCharSuffixEmitter : ISuffixEmitter
{
    private readonly string _strSuffixChar;

    public char SuffixChar { get; }
    public bool CheckValueEnd { get; }

    public SingleCharSuffixEmitter(char suffixChar, bool checkValueEnd = true)
    {
        SuffixChar     = suffixChar;
        CheckValueEnd  = checkValueEnd;
        _strSuffixChar = suffixChar.ToString();
    }

    public string EmitSuffix(IReadOnlyList<string> words)
    {
        if ( CheckValueEnd && words[^1][^1] == SuffixChar )
            return string.Empty;

        return _strSuffixChar;
    }
}
