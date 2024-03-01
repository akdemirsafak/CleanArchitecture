using System.Reflection;

namespace CleanArchitecture.Persistance;

public static class PersistanceAssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}
