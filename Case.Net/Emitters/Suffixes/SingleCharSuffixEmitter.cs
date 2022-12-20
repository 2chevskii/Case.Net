namespace Case.Net.Emitters.Suffixes;

public sealed class SingleCharSuffixEmitter : ISuffixEmitter
{
    private readonly string _strSuffixChar;

    public char SuffixChar { get; }
    public bool CheckValueEnd { get; }

    public SingleCharSuffixEmitter(char suffixChar, bool checkValueEnd = true)
    {
        SuffixChar = suffixChar;
        CheckValueEnd = checkValueEnd;
        _strSuffixChar = suffixChar.ToString();
    }

    public bool EmitSuffix(IReadOnlyList<string> words, out ReadOnlySpan<char> suffixBuffer)
    {
        if ( CheckValueEnd && words[^1][^1] == SuffixChar )
        {
            suffixBuffer = ReadOnlySpan<char>.Empty;

            return false;
        }

        suffixBuffer = _strSuffixChar.AsSpan();

        return true;
    }
}
