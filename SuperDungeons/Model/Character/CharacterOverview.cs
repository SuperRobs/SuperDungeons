using System.ComponentModel;
using SuperDungeons.Model.Classes;

namespace SuperDungeons.Model.Character;

public class CharacterOverview : BindableObject
{
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    
    private bool _hasInspiration;
    private string _name;
    private readonly Character _character;

    public CharacterOverview(string name, Character character)
    {
        _name = name;
        _character = character;
        character.ClassManager.PropertyChanged += ClassChanged;
    }

    public bool HasInspiration
    {
        get => _hasInspiration;
        set
        {
            _hasInspiration = value;
            OnPropertyChanged(nameof(HasInspiration));
        }
    }

    //this thing throws a lot of warning (two ReSharper and one pragma, wow) but this will all be removed when
    //it is actually implemented
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once MemberCanBeMadeStatic.Global
#pragma warning disable CA1822
    public uint GetCharacterLevel()
#pragma warning restore CA1822
    {
        return _character.ClassManager.GetCombinedClassLevel();
    }
    
    public uint GetProficiencyBonus()
    {
        //the formula is a bit unintuitive, but for every fourth level, but starting with the fifth (as class levels
        //start at one instead of zero) it increases by one, the proficiency bonus also starts at 2
        return 2 + (GetCharacterLevel() - 1) / 4;
    }

    private void ClassChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case null:
                return;
            case nameof(ClassManager.GetCombinedClassLevel):
                OnPropertyChanged(nameof(GetProficiencyBonus));
                OnPropertyChanged(nameof(GetCharacterLevel));
                break;
        }
    }
}