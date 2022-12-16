using System.Text;

using Case.Net.Emit.Concatenation;
using Case.Net.Emit.Prefixes;
using Case.Net.Emit.Suffixes;
using Case.Net.Emit.Words;
using Case.Net.Extensions;

namespace Case.Net.Common.Conventions;

public class GenericNamingConvention : INamingConvention
{
    public string Name { get; }
    public IWordConcatenator? WordConcatenator { get; }
    public IWordEmitter? WordEmitter { get; }
    public IPrefixEmitter? PrefixEmitter { get; }
    public ISuffixEmitter? SuffixEmitter { get; }

    public GenericNamingConvention(
        string name,
        IWordConcatenator? wordConcatenator,
        IWordEmitter? wordEmitter,
        IPrefixEmitter? prefixEmitter,
        ISuffixEmitter? suffixEmitter
    )
    {
        if ( string.IsNullOrWhiteSpace( name ) )
            throw new ArgumentNullException( nameof( name ) );

        Name = name;
        WordConcatenator = wordConcatenator;
        WordEmitter = wordEmitter;
        PrefixEmitter = prefixEmitter;
        SuffixEmitter = suffixEmitter;
    }

    /*public unsafe CasedString Convert(CasedString source)
    {
        Span<char> value;

        if ( WordEmitter is not null )
        {
            List<string> words = new ();

            for ( int i = 0; i < source.WordCount(); i++ )
            {
                words.Add( WordEmitter.EmitWord( source, i ) );
            }

            List<string> concatenators = new ();

            if ( words.Count > 1 && WordConcatenator is not null  )
            {
                for ( int i = 1; i < words.Count-1; i++ )
                {
                    concatenators.Add( WordConcatenator.GetConcatenation( words[i], words[i+1] ) );
                }
            }

            int totalLength = words.Sum( x => x.Length ) + concatenators.Sum( x => x.Length );

            value = stackalloc char[totalLength];

            int totalCopied = 0;

            for ( var i = 0; i < words.Count; i++ )
            {
                var slice = value.Slice( totalCopied );
                var word  = words[0];

                word.CopyTo( slice );

                totalCopied += word.Length;

                if ( words.Count - i < 1 )
                {
                    var slice2       = slice.Slice( word.Length );
                    var concatenator = concatenators[i];

                    concatenator.CopyTo( slice2 );
                    totalCopied += concatenator.Length;
                }
            }
        }
        else
        {
            value = stackalloc char[0];
        }

        var prefix = GetPrefix( value );
        var suffix = GetSuffix( value );

        var totalStringLength = prefix.Length + suffix.Length + value.Length;

        Span<char> stringValue = stackalloc char[totalStringLength];

        prefix.CopyTo(stringValue);
        value.CopyTo(stringValue.Slice(prefix.Length));
        suffix.CopyTo(stringValue.Slice(prefix.Length+value.Length));

        var casedString = new CasedString( new string( stringValue ), source.Words, this  );

        return casedString;
    }*/

    public CasedString Convert(CasedString source)
    {
        var words                      = EmitWords( source );
        var concatenators              = EmitConcatenators( words );
        var concatenatedValueCharCount = words.CharCount() + concatenators.CharCount();

        ReadOnlySpan<char> concatenatedValue = stackalloc char[concatenatedValueCharCount];
        var                prefix            = EmitPrefix( concatenatedValue );
        var                suffix            = EmitSuffix( concatenatedValue );



        var fullValue = prefix.Add( concatenatedValue, suffix );

    }

    ReadOnlySpan<char> EmitPrefix(ReadOnlySpan<char> value) { }

    ReadOnlySpan<char> EmitSuffix(ReadOnlySpan<char> value) { }

    List<Word> EmitWords(CasedString source)
    {
        List<Word> words = new ();

        if ( WordEmitter is not null )
        {
            for ( int i = 0; i < source.WordCount(); i++ )
            {
                words.Add( WordEmitter.EmitWord( source, i ) );
            }
        }

        return words;
    }

    List<string> EmitConcatenators(List<Word> words)
    {
        var concatenators = new List<string>();

        if ( WordConcatenator is not null )
        {
            for ( int i = 0; i < words.Count - 1; i++ )
            {
                concatenators.Add(
                    WordConcatenator.GetConcatenation( words[i].Value, words[i + 1].Value )
                );
            }

        }

        return concatenators;
    }

    ReadOnlySpan<char> GetPrefix(ReadOnlySpan<char> value)
    {
        if ( PrefixEmitter is not null )
        {
            return PrefixEmitter.GetPrefix( value );
        }

        return ReadOnlySpan<char>.Empty;
    }

    ReadOnlySpan<char> GetSuffix(ReadOnlySpan<char> value)
    {
        if ( SuffixEmitter is not null )
        {
            return SuffixEmitter.GetSuffix( value );
        }

        return ReadOnlySpan<char>.Empty;
    }
}
