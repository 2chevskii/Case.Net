using System.Collections.Specialized;
using System.Net.Sockets;

using Case.Net.Common.Conventions;

namespace Case.Net.Common;

public interface INamingManager : IEnumerable<INamingConvention>, ICollection<INamingConvention>
{
    public INamingConvention Detect(ReadOnlySpan<char> input);

    public CasedString Parse(ReadOnlySpan<char> input);
}
