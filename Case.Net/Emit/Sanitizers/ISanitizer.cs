namespace Case.Net.Emit.Sanitizers;

public interface ISanitizer
{
    string SanitizeWord(string word);

    string SanitizePrefix(string prefix);

    string SanitizeSuffix(string suffix);
}
