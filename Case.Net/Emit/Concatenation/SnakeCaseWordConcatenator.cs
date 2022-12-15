namespace Case.Net.Emit.Concatenation;

public class SnakeCaseWordConcatenator : IWordConcatenator
{
    private const string  UNDERSCORE = "_";

    public string GetConcatenation(string current, string next) => UNDERSCORE;
}
