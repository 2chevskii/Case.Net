using System.Collections;
using System.Reflection;

using Case.Net.Common.Conventions;
using Case.Net.Common.Entities;

namespace Case.Net.Common.NamingManagers;

/// <summary>
/// Contains <see cref="INamingConvention"/>s from all loaded assemblies
/// </summary>
public sealed class GlobalNamingManager : INamingManager
{
    Dictionary<string, INamingConvention> _conventions;

    public GlobalNamingManager() { _conventions = new Dictionary<string, INamingConvention>(); }

    static IEnumerable<INamingConvention> LoadFromAssembly(Assembly assembly)
    {
        IEnumerable<Type> conventions = assembly.GetTypes()
                                                .Where(
                                                    type =>
                                                    type.IsAssignableTo(
                                                        typeof( INamingConvention )
                                                    ) &&
                                                    type.IsClass &&
                                                    !type.IsAbstract &&
                                                    type.GetConstructors()
                                                        .Any(
                                                            ctor => ctor.IsPublic &&
                                                            ctor.GetParameters().Length is 0
                                                        )
                                                );

        foreach ( Type type in conventions )
        {
            INamingConvention convention = (INamingConvention) Activator.CreateInstance( type )!;

            yield return convention;
        }
    }

    public IEnumerator<INamingConvention> GetEnumerator() { throw new NotImplementedException(); }

    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    public void Add(INamingConvention item) { throw new NotImplementedException(); }

    public void Clear() { throw new NotImplementedException(); }

    public bool Contains(INamingConvention item) { throw new NotImplementedException(); }

    public void CopyTo(INamingConvention[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(INamingConvention item) { throw new NotImplementedException(); }

    public int Count { get; }
    public bool IsReadOnly { get; }

    public INamingConvention Detect(ReadOnlySpan<char> input)
    {
        throw new NotImplementedException();
    }

    public CasedString Parse(ReadOnlySpan<char> input) { throw new NotImplementedException(); }
}
