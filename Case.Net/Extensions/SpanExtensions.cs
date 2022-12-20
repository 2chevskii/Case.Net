namespace Case.Net.Extensions;

public static class SpanExtensions
{
    public static bool Any<T>(this Span<T> self, Predicate<T> predicate)
    {
        for ( int i = 0; i < self.Length; i++ )
        {
            T? entry = self[i];

            if ( predicate( entry ) )
                return true;
        }

        return false;
    }

    public static bool All<T>(this Span<T> self, Predicate<T> predicate)
    {
        for ( int i = 0; i < self.Length; i++ )
        {
            T? entry = self[i];

            if ( !predicate( entry ) )
                return false;
        }

        return true;
    }

    public static T MinBy<T, V>(this Span<T> self, Func<T, V> valueSelector, out int index)
    {
        if ( self.Length == 0 )
        {
            throw new InvalidOperationException( "Sequence contains not elements" );
        }

        if ( self.Length == 1 )
        {
            index = 0;
            return self[0];
        }

        Comparer<V>? comparer = Comparer<V>.Default;
        V   minV     = valueSelector( self[0] );
        int minIndex = 0;

        for ( int i = 1; i < self.Length ; i++ )
        {
            T? current  = self[i];
            V? currentV = valueSelector( current );

            if ( comparer.Compare( currentV, minV ) is -1 )
            {
                minV = currentV;
                minIndex = i;
            }
        }


        index = minIndex;
        return self[minIndex];
    }
}
