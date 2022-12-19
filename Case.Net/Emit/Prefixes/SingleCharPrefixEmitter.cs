namespace Case.Net.Emit.Prefixes;

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

    public string EmitPrefix(IReadOnlyList<string>words)
    {
        if ( CheckValueStart && words[0][0] == PrefixChar )
            return string.Empty;

        return _strPrefixChar;
    }
}
