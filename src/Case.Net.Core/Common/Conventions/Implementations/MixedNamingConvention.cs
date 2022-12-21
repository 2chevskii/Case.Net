using System;

using Case.Net.Common.Entities;
using Case.Net.Extensions;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions
{
    public class MixedNamingConvention : INamingConvention
    {
        private readonly IWordParser[] _parsers;

        public string Name => "Mixed";

        public MixedNamingConvention()
        {

        }

        public bool TryConvert(CasedString input, out CasedString output)
        {
            output = input;

            return false;
        }

        public CasedString Convert(CasedString source)
        {
            throw new InvalidOperationException();
        }

        public CasedString Parse(ReadOnlySpan<char> input)
        {
            if ( !TryParse( input, out CasedString output ) )
            {
                throw new Exception();
            }

            return output;
        }

        public bool TryParse(ReadOnlySpan<char> input, out CasedString output)
        {
            // parse possible prefix
            string prefix = string.Empty;
            int    i      = 0;

            while ( i < input.Length )
            {

            }


            /*for ( var i = 0; i < _parsers.Length; i++ )
            {
                var parser = _parsers[i];

                if ( parser.TryParse( input, out var wordPositions ) )
                {
                    input.SplitWithWordPositions(
                        wordPositions,
                        out var words,
                        out var delimiters
                    );

                    var str = new CasedString(
                        string.Empty,
                        string.Empty,
                        words,
                        delimiters,
                        this
                    );

                    output = str;

                    return true;
                }
            }

            output = CasedString.Empty;
            return false;*/
        }

    }
}
