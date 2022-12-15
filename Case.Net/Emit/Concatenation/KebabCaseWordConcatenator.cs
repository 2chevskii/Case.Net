namespace Case.Net.Emit.Concatenation;

public class KebabCaseWordConcatenator : IWordConcatenator
{
    private const string DASH = "-";

    public string GetConcatenation(string current, string next) => DASH;
}
