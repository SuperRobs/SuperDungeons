using System.ComponentModel;
using System.Diagnostics;
using SuperDungeons.Model.Features;
using SuperDungeons.Utils;

namespace SuperDungeons.Model.Classes;

public class ClassManager : BindableObject
{
    private readonly List<ClassData> _classes = [];
    private readonly ChoiceManager _choiceManager = new();
    
    public uint GetProficiencyBonus()
    {
        return 2 + (GetCharacterLevel() - 1) / 4;
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public uint GetCharacterLevel()
    {
        //unfortunately Sum doesn't work with uint, so I need to convert it before that and convert it back later
        return (uint) _classes.Select(c => (int) c.Level).Sum();
    }

    public List<string> GetClassNames()
    {
        return _classes.Select(c => c.GetClassName()).ToList();
    }

    public uint GetClassLevel(string className)
    {
        return _classes.FirstOrDefault(c => c.GetClassName().Equals(className))?.Level ?? 0;
    }

    public string GetSubclass(string className)
    {
        return _classes.FirstOrDefault(c => c.GetClassName().Equals(className))?.GetSubclassName() 
               ?? string.Empty;
    }
    
    private void ClassChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(e.PropertyName));
    }
    
    /// <summary>
    /// adds the class to the character's list of classes
    /// if the class already existed nothing happened, if the specified class does not exist nothing happens either
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
        var @class = ClassUtils.GetClass(name);
        if (@class is null)
        {
            Debug.WriteLine($"Class {name} does not exist");
            return;
        }

        var data = ClassData.Create(@class, level, _choiceManager);
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
        if (ClassExists(name)) return;
        FindClassData(name).Level = level;
    }

    private bool ClassExists(string name)
    {
        return _classes.Any(c => c.GetClassName().Equals(name));
    }
    
    private ClassData FindClassData(string name)
    {
        return _classes.First(c => c.GetClassName().Equals(name));
    }

    public List<IFeature> GetFeatures()
    {
        return _classes.SelectMany(c => c.GetClassFeatures()).ToList();
    }
}