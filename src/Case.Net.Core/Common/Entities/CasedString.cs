using System.Collections.Generic;
using System.Linq;
using System.Text;

using Case.Net.Common.Conventions;
using Case.Net.Extensions;
using Case.Net.Internal;

using static Case.Net.Internal.CollectionUtils;

namespace Case.Net.Common.Entities
{
    public readonly struct CasedString
    {
        public static readonly CasedString Empty = new CasedString(
            string.Empty,
            string.Empty,
            EmptyArray<string>(),
            EmptyArray<Delimiter>(),
            new MixedNamingConvention()
        );

        public string Prefix { get; }
        public string Suffix { get; }
        public IReadOnlyList<string> Words { get; }
        public IReadOnlyList<Delimiter> Delimiters { get; }
        public INamingConvention NamingConvention { get; }

        public CasedString(
            string prefix,
            string suffix,
            IEnumerable<string> words,
            IEnumerable<Delimiter> delimiters,
            INamingConvention namingConvention
        )
        {
            Prefix           = prefix;
            Suffix           = suffix;
            Words            = words.ToArray();
            Delimiters       = delimiters.ToArray();
            NamingConvention = namingConvention;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = StringBuilderPool.Get();

            AppendPrefix( stringBuilder );
            AppendConcatenatedWords( stringBuilder );
            AppendSuffix( stringBuilder );

            string value = stringBuilder.ToString();

            StringBuilderPool.Return( ref stringBuilder );

            return value;
        }

        private void AppendPrefix(StringBuilder stringBuilder)
        {
            if ( Prefix.Length != 0 ) { stringBuilder.Append( Prefix ); }
        }

        private void AppendSuffix(StringBuilder stringBuilder)
        {
            if ( Suffix.Length != 0 ) { stringBuilder.Append( Suffix ); }
        }

        private void AppendConcatenatedWords(StringBuilder stringBuilder)
        {
            if ( !Words.Any() )
                return;

            for ( int i = 0; i < Words.Count - 1; i++ )
            {
                stringBuilder.Append( Words[i] );

                if ( this.HasDelimiterFor( i ) )
                {
                    stringBuilder.Append( this.GetDelimiterFor( i ) );
                }
            }

            stringBuilder.Append( Words[Words.Count - 1] );
        }
    }
}
