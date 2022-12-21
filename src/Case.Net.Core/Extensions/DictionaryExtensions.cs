using System.Collections.Generic;

namespace Case.Net.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool TryAdd<K, V>(this Dictionary<K, V> self, K key, V value)
        {
            if ( self.ContainsKey( key ) )
                return false;

            self.Add( key, value );

            return true;
        }
    }
}
