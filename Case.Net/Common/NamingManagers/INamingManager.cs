using Case.Net.Common.Conventions;
using Case.Net.Common.Entities;

namespace Case.Net.Common.NamingManagers;

public interface INamingManager : IEnumerable<INamingConvention>, ICollection<INamingConvention>
{
    public INamingConvention Detect(ReadOnlySpan<char> input);

    public CasedString Parse(ReadOnlySpan<char> input);
}
