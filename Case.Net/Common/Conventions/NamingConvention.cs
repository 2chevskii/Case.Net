namespace Case.Net.Common.Conventions;

public abstract class NamingConvention : INamingConvention
{
    public virtual string Name { get; }

    public NamingConvention(string name)
    {
        if ( string.IsNullOrWhiteSpace( name ) )
            throw new ArgumentNullException( nameof( name ) );

        Name = name;
    }

    public virtual CasedString Convert(CasedString input)
    {
        if ( !TryConvert( input, out var output ) ) { throw new Exception(); }

        return output;
    }

    public abstract bool TryConvert(CasedString input, out CasedString output);

    public virtual CasedString Parse(ReadOnlySpan<char> input)
    {
        if ( !TryParse( input, out var output ) ) { throw new Exception(); }

        return output;
    }

    public abstract bool TryParse(ReadOnlySpan<char> input, out CasedString output);
}
