using Case.Net.Common.Entities;

namespace Case.Net.Common.Conventions;

public abstract class NamingConvention : INamingConvention
{
    public virtual string Name { get; }

    protected NamingConvention(string name)
    {
        if ( string.IsNullOrWhiteSpace( name ) )
            throw new ArgumentNullException( nameof( name ) );

        Name = name;
    }

    public abstract bool TryConvert(CasedString input, out CasedString output);

    public abstract bool TryParse(ReadOnlySpan<char> input, out CasedString output);

    public virtual CasedString Convert(CasedString input)
    {
        if ( !TryConvert( input, out CasedString output ) )
        {
            throw new Exception();
        }

        return output;
    }

    public virtual CasedString Parse(ReadOnlySpan<char> input)
    {
        if ( !TryParse( input, out CasedString output ) )
        {
            throw new Exception();
        }

        return output;
    }

    public override string ToString() => $"NamingConvention[{Name}]";
}
