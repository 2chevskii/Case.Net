using Case.Net.Emitters.Delimiters;
using Case.Net.Emitters.Prefixes;
using Case.Net.Emitters.Sanitizers;
using Case.Net.Emitters.Suffixes;
using Case.Net.Emitters.Words;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions
{
    public class ConstantCaseNamingConvention : GenericNamingConvention
    {
        public ConstantCaseNamingConvention() : base(
            "CONSTANT_CASE",
            new AllUpperWordEmitter( new LetterOrDigitSanitizer() ),
            new NoPrefixEmitter(),
            new NoSuffixEmitter(),
            new SingleCharDelimiterEmitter( '_' ),
            new DelimiterUpperCaseParser( '_' ),
            false
        ) { }
    }
}
