using Autofac;

namespace Model.Composition;

public static class CompositionRoot
{
    public static ContainerBuilder CreateBuilder()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<CoreModule>();
        return builder;
    }
}