using System.ComponentModel;

namespace SuperDungeons.Model.Classes;

public class ClassManager : BindableObject
{
    private readonly ISet<Class> _classes = new HashSet<Class>();

    public uint GetProficiencyBonus()
    {
        return 2 + (GetCharacterLevel() - 1) / 4;
    }
    
    public uint GetCharacterLevel()
    {
        return _classes.Aggregate((uint)0, (level, @class) => level + @class.ClassLevel);
    }

    private void ClassesChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(GetCharacterLevel));
        OnPropertyChanged(nameof(GetProficiencyBonus));
    }
}