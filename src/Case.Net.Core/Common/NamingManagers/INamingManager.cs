using System;
using System.Collections.Generic;

using Case.Net.Common.Conventions;
using Case.Net.Common.Entities;

namespace Case.Net.Common.NamingManagers
{
    public interface INamingManager : IEnumerable<INamingConvention>, ICollection<INamingConvention>
    {
        INamingConvention Detect(ReadOnlySpan<char> input);

        CasedString Parse(ReadOnlySpan<char> input);
    }
}
