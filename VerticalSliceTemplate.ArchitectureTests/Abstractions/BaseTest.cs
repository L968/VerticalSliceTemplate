using System.Reflection;

namespace VerticalSliceTemplate.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(Application.DependencyInjection).Assembly;
}
