namespace Case.Net.Common.Conventions;

public static class NamingConventions
{
    public static INamingConvention Detect(
        Prefix prefix,
        Suffix suffix,
        IReadOnlyList<Word> words,
        IReadOnlyList<Delimiter> delimiters
    )
    {
        return new MixedNamingConvention();
    }
}
