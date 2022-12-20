using System.Collections;

using Case.Net.Common.Conventions;
using Case.Net.Common.Entities;

namespace Case.Net.Common.NamingManagers;

public class NamingManager : INamingManager
{
    private readonly Dictionary<string, INamingConvention> _conventions;

    public int Count => _conventions.Count;
    public bool IsReadOnly => false;
    public IReadOnlyCollection<string> Names => _conventions.Keys;
    public IReadOnlyCollection<INamingConvention> Values => _conventions.Values;

#region Ctors

    public NamingManager() { _conventions = new Dictionary<string, INamingConvention>(); }

    public NamingManager(IEnumerable<INamingConvention> conventions) : this()
    {
        foreach ( INamingConvention convention in conventions )
        {
            _ = Add( convention );
        }
    }

    public NamingManager(params INamingConvention[] conventions) : this(
        (IEnumerable<INamingConvention>) conventions
    ) { }

#endregion

    public IEnumerator<INamingConvention> GetEnumerator() => Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Add(INamingConvention convention) =>
    _conventions.TryAdd( convention.Name, convention );

    void ICollection<INamingConvention>.Add(INamingConvention convention) => Add( convention );

    public void Clear() => _conventions.Clear();

    public bool Contains(string conventionName)
    {
        return _conventions.ContainsKey( conventionName );
    }

    bool ICollection<INamingConvention>.Contains(INamingConvention convention) =>
    Contains( convention.Name );

    public bool Remove(string conventionName) => _conventions.Remove( conventionName );

    bool ICollection<INamingConvention>.Remove(INamingConvention convention) =>
    Remove( convention.Name );

    public void CopyTo(INamingConvention[] buffer, int index)
    {
        INamingConvention[]? source = Values.ToArray();

        int length = Math.Min( buffer.Length, source.Length );

        for ( int i = index; i < length; i++ )
        {
            int sourceIndex = i - index;
            buffer[i] = source[sourceIndex];
        }
    }

    public INamingConvention Detect(ReadOnlySpan<char> input)
    {
        return Parse( input ).NamingConvention;
    }

    public CasedString Parse(ReadOnlySpan<char> input)
    {
        foreach ( INamingConvention namingConvention in Values )
        {
            if ( namingConvention.TryParse( input, out CasedString output ) )
            {
                return output;
            }
        }

        throw new Exception( "Failed to parse with all conventions" );
    }
}
