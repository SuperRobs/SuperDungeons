using System.ComponentModel;
using System.Diagnostics;
using SuperDungeons.Model.Features;
using SuperDungeons.Utils;

namespace SuperDungeons.Model.Classes;

public class ClassManager(ClassRepository repository, ChoiceManager choiceManager) : BindableObject, IClassManager
{
    //first class is primary
    private readonly List<CharacterClass> _classes = [];

    public uint GetCharacterProficiencyBonus()
    {
        return 2 + (GetCharacterLevel() - 1) / 4;
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public uint GetCharacterLevel()
    {
        //unfortunately Sum doesn't work with uint, so I need to convert it before that and convert it back later
        return (uint) _classes.Select(c => (int) c.Level).Sum();
    }

    public List<string> GetCharacterClassNames()
    {
        return _classes.Select(c => c.GetClassName()).ToList();
    }

    public uint GetCharacterClassLevel(string className)
    {
        return _classes.FirstOrDefault(c => c.GetClassName().Equals(className))?.Level ?? 0;
    }

    public string GetCharacterClassSubclass(string className)
    {
        return _classes.FirstOrDefault(c => c.GetClassName().Equals(className))?.GetSubclassName()
               ?? string.Empty;
    }

    public HashSet<CharacterClassOverview> GetAllClassInfo()
    {
        return GetCharacterClassNames()
            .Select(c => new CharacterClassOverview(c, GetCharacterClassLevel(c), GetCharacterClassSubclass(c)))
            .ToHashSet();
    }
    
    //catch updates from CharacterClass objects and distribute them further
    private void ClassChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(e.PropertyName));
    }
    
    /// <summary>
    /// adds the class to the character's list of classes
    /// if the class already existed nothing happened, if the specified class does not exist, nothing happens either
    /// </summary>
    /// <param name="name"></param>
    /// <param name="level"></param>
    public void AddClass(string name, uint level)
    {
        if (_classes.Any(c => c.GetClassName().Equals(name)))
        {
            Debug.WriteLine($"Class {name} already exists");
            return;
        }
        var @class = repository.GetClass(name);
        if (@class is null)
        {
            Debug.WriteLine($"Class {name} does not exist");
            return;
        }

        var data = CharacterClass.Create(@class, level, choiceManager, repository);
        data.PropertyChanged += ClassChanged;
        _classes.Add(data);
    }

    /// <summary>
    /// updates the class to the specified level
    /// does nothing if the class does not exist
    /// </summary>
    /// <param name="name"></param>
    /// <param name="level"></param>
    public void UpdateClass(string name, uint level)
    {
        if (level == 0)
        {
            RemoveClass(name);
            return;
        }
        _classes.First(c => c.GetClassName().Equals(name)).Level = level;
    }

    public void RemoveClass(string name)
    {
        var @class = _classes.FirstOrDefault(c => c.GetClassName().Equals(name));
        if (@class is null) return;
        @class.PropertyChanged -= ClassChanged;
        _classes.RemoveAll(c => c.GetClassName().Equals(name));
    }
}