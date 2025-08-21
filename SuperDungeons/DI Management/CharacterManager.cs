using Autofac;

namespace SuperDungeons.DI_Management;

public class CharacterManager
{
    private readonly ILifetimeScope _rootScope;

    private ILifetimeScope _currentScope;

    public CharacterManager(ILifetimeScope rootScope)
    {
        _rootScope = rootScope;
    }

    public void LoadCharacter(string characterId)
    {
        //ToDo actually implement this
        //placeholder for now
        _currentScope?.Dispose();
    }
}