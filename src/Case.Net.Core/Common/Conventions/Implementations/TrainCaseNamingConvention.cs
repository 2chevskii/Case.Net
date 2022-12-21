using Case.Net.Emitters.Delimiters;
using Case.Net.Emitters.Prefixes;
using Case.Net.Emitters.Sanitizers;
using Case.Net.Emitters.Suffixes;
using Case.Net.Emitters.Words;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions
{
    public sealed class TrainCaseNamingConvention : GenericNamingConvention
    {
        public TrainCaseNamingConvention() : base(
            "Train-Case",
            new FirstUpperWordEmitter( new LetterOrDigitSanitizer() ),
            new NoPrefixEmitter(),
            new NoSuffixEmitter(),
            new SingleCharDelimiterEmitter( '-' ),
            new DelimiterFirstUpperParser( '-' ),
            false
        ) { }
    }
}
