using Case.Net.Emitters.Delimiters;
using Case.Net.Emitters.Prefixes;
using Case.Net.Emitters.Sanitizers;
using Case.Net.Emitters.Suffixes;
using Case.Net.Emitters.Words;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions
{
    public class NamespaceCaseNamingConvention : GenericNamingConvention
    {
        public NamespaceCaseNamingConvention() : base(
            "Namespace.Case",
            new FirstUpperWordEmitter( new LetterOrDigitSanitizer() ),
            new NoPrefixEmitter(),
            new NoSuffixEmitter(),
            new SingleCharDelimiterEmitter( '.' ),
            new DelimiterFirstUpperParser( '.' ),
            false
        ) { }
    }
}
