using System;

namespace Case.Net.Parsing.Suffix;

public interface ISuffixParser
{
    bool TryParseSuffix(ReadOnlySpan<char> input, ReadOnlySpan<char> suffix);
}
