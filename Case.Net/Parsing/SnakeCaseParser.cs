namespace Case.Net.Parsing;

public sealed class SnakeCaseParser : DelimiterLowerCaseParser
{

    public SnakeCaseParser() : base( '_' ) { }
}

public sealed class KebabCaseParser : DelimiterLowerCaseParser
{
    public KebabCaseParser() : base( '-' ) { }
}
