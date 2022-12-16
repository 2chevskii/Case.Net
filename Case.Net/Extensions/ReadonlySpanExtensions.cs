namespace Case.Net.Extensions;

public static class ReadonlySpanExtensions
{
    public static ReadOnlySpan<T> Add<T>(this ReadOnlySpan<T> self, ReadOnlySpan<T> other)
    where T : unmanaged
    {
        int resultLength = self.Length + other.Length;

        T[]     array  = new T[resultLength];
        Span<T> result = new Span<T>( array );

        self.CopyTo( result );
        other.CopyTo( result[self.Length..] );

        return result;
    }

    public static ReadOnlySpan<T> Add<T>(
        this ReadOnlySpan<T> self,
        ReadOnlySpan<T> other1,
        ReadOnlySpan<T> other2
    ) where T : unmanaged
    {
        int resultLength = self.Length + other1.Length + other2.Length;

        T[]     array  = new T[resultLength];
        Span<T> result = new Span<T>( array );

        self.CopyTo( result );
        other1.CopyTo( result[self.Length..] );
        other2.CopyTo( result[(self.Length + other1.Length)..] );

        return result;
    }
}
