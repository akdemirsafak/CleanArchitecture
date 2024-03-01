using System.Reflection;

namespace CleanArchitecture.Application;

public static class ApplicationAssemblyReference
{
    public static readonly Assembly Assembly= typeof(Assembly).Assembly;
}
