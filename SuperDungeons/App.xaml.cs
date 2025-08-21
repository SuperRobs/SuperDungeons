using System.Windows;
using SuperDungeons.DI_Management;

namespace SuperDungeons;

using Autofac;

public partial class App
{
    public IContainer DiContainer { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        //Setup DI
        var builder = new ContainerBuilder();

        builder.RegisterType<CharacterManager>().SingleInstance();
        DiContainer = builder.Build();
    }
}