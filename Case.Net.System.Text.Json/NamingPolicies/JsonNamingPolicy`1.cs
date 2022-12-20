using Case.Net.Common;
using Case.Net.Common.Conventions;

namespace System.Text.Json;

public class JsonNamingPolicy<TConvention> : JsonNamingPolicy
where TConvention : INamingConvention, new()
{
    readonly TConvention _convention;

    public JsonNamingPolicy() { _convention = new TConvention(); }

    public override string ConvertName(string name)
    {
        CasedString parsed    = NamingConventions.Parse( name.AsSpan() );
        CasedString converted = _convention.Convert( parsed );

        return converted.ToString()!;
    }
}
