global using static Case.Net.Extensions.HelperExtensions;

using System.Runtime.CompilerServices;

namespace Case.Net.Extensions;

public static class HelperExtensions
{
    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static T[] EmptyArray<T>() { return Array.Empty<T>(); }
}
