namespace Case.Net.Emitters.Prefixes;

public class SingleCharPrefixEmitter : IPrefixEmitter
{
    private readonly string _strPrefixChar;

    public char PrefixChar { get; }
    public bool CheckValueStart { get; }

    public SingleCharPrefixEmitter(char prefixChar, bool checkValueStart = true)
    {
        PrefixChar      = prefixChar;
        CheckValueStart = checkValueStart;
        _strPrefixChar  = prefixChar.ToString();
    }

    public bool EmitPrefix(IReadOnlyList<string>words, out ReadOnlySpan<char> prefixBuffer)
    {
        if ( CheckValueStart && words[0][0] == PrefixChar )
        {
            prefixBuffer = ReadOnlySpan<char>.Empty;
            return false;
        }

        prefixBuffer = _strPrefixChar;

        return true;
    }
}
