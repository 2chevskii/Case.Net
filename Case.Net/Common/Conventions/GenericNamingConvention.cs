using Case.Net.Emitters.Delimiters;
using Case.Net.Emitters.Prefixes;
using Case.Net.Emitters.Suffixes;
using Case.Net.Emitters.Words;
using Case.Net.Extensions;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions;

public class GenericNamingConvention : NamingConvention
{
    public IPrefixEmitter PrefixEmitter { get; }
    public ISuffixEmitter SuffixEmitter { get; }
    public IWordEmitter WordEmitter { get; }
    public IDelimiterEmitter DelimiterEmitter { get; }
    public IWordParser Parser { get; }
    public bool AcceptEmptyInput { get; }

    public GenericNamingConvention(
        string name,
        IWordEmitter wordEmitter,
        IPrefixEmitter prefixEmitter,
        ISuffixEmitter suffixEmitter,
        IDelimiterEmitter delimiterEmitter,
        IWordParser parser,
        bool acceptEmptyInput
    ) : base( name )
    {
        PrefixEmitter = prefixEmitter;
        SuffixEmitter = suffixEmitter;
        WordEmitter = wordEmitter;
        DelimiterEmitter = delimiterEmitter;
        Parser = parser;
        AcceptEmptyInput = acceptEmptyInput;
    }

    public override bool TryConvert(CasedString input, out CasedString output)
    {
        if ( input.IsEmpty() )
        {
            if ( AcceptEmptyInput )
            {
                output = new CasedString(
                    string.Empty,
                    string.Empty,
                    EmptyArray<string>(),
                    EmptyArray<Delimiter>(),
                    this
                );
            }
            else
            {
                output = CasedString.Empty;
            }

            return AcceptEmptyInput;
        }

        List<string> words = new List<string>( input.WordCount() );

        for ( int i = 0; i < input.WordCount(); i++ )
        {
            if ( WordEmitter.EmitWord( input, i, out var wordBuffer ) )
            {
                words.Add( wordBuffer.ToString() );
            }
        }

        string prefix = string.Empty;

        if ( PrefixEmitter.EmitPrefix( words, out var prefixBuffer ) )
        {
            prefix = prefixBuffer.ToString();
        }

        string suffix = string.Empty;

        if ( SuffixEmitter.EmitSuffix( words, out var suffixBuffer ) )
        {
            suffix = suffixBuffer.ToString();
        }

        List<Delimiter> delimiters = new ( words.Count - 1 );

        for ( int i = 0; i < words.Count - 1; i++ )
        {
            if ( !DelimiterEmitter.EmitDelimiter( words, i, out var delimiterBuffer ) )
            {
                continue;
            }

            delimiters.Add( new Delimiter( i, delimiterBuffer.ToString() ) );
        }

        output = new CasedString( prefix, suffix, words, delimiters, this );

        return true;
    }

    public override bool TryParse(ReadOnlySpan<char> input, out CasedString output)
    {
        if ( input.IsEmpty )
        {
            if ( AcceptEmptyInput )
            {
                output = new CasedString(
                    string.Empty,
                    string.Empty,
                    EmptyArray<string>(),
                    EmptyArray<Delimiter>(),
                    this
                );
            }
            else
            {
                output = CasedString.Empty;
            }

            return AcceptEmptyInput;
        }

        /*if ( !new SnakeCaseParser().TryParse( input, out var wordPositions ) )
        {
            output = CasedString.Empty;

            return false;
        }

        input.SplitWithWordPositions( wordPositions, out var words, out var delimiters );

        output = new CasedString( string.Empty, string.Empty, words, delimiters, this );

        return true;*/

        if ( !Parser.TryParse( input, out var wordPositions ) )
        {
            output = CasedString.Empty;

            return false;
        }

        input.SplitWithWordPositions( wordPositions, out var words, out var delimiters );

        output = new CasedString( string.Empty, string.Empty, words, delimiters, this );

        return true;
    }
}
