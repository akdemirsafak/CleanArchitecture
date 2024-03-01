using System.Reflection;

namespace CleanArchitecture.Presentation;

public static class PresentationAssemblyReference //Presentation katmanının dll haline erişim sağlama
{
    public static readonly Assembly assembly = typeof(Assembly).Assembly;
}
