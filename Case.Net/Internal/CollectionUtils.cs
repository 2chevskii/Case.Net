global using static Case.Net.Internal.CollectionUtils;

using System.Runtime.CompilerServices;

namespace Case.Net.Internal;

public static class CollectionUtils
{
    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static T[] EmptyArray<T>() => Array.Empty<T>();

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static ReadOnlySpan<T> EmptyReadOnlySpan<T>() => ReadOnlySpan<T>.Empty;

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Span<T> EmptySpan<T>() => Span<T>.Empty;
}
