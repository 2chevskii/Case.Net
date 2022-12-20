using System.Text;

using Microsoft.Extensions.ObjectPool;

namespace Case.Net.Internal;

internal static class StringBuilderPool
{
    static readonly ObjectPool<StringBuilder> s_pool;

    static StringBuilderPool()
    {
        s_pool = new DefaultObjectPoolProvider().CreateStringBuilderPool();
    }

    public static StringBuilder Get() { return s_pool.Get(); }

    public static void Return(ref StringBuilder stringBuilder)
    {
        /*return the object to the pool and nullify the given reference*/
        /*ofc it does not ensure that no other references exist, but provides a sense of security*/
        s_pool.Return( stringBuilder );
        stringBuilder = null!;
    }
}
