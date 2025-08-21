using SuperDungeons.Utils;

namespace SuperDungeons.Model.Character.Classes;

public class CharacterClasses : BindableObject, ICharacterClasses
{
    //first class in this list is primary class, beyond that it is arbitrarily ordered
    private List<CharacterClass> _classes = [];

    public uint GetProficiencyBonus()
    {
        return 2 + (GetCharacterLevel() - 1) / 4;
    }
    
    public uint GetCharacterLevel()
    {
        //unfortunately Sum doesn't work with uint, so I need to cast it back and forth here
        return (uint) _classes.Select(c => (int) c.Level).Sum();
    }

    public List<string> GetClassNames()
    {
        return _classes.Select(c => c.ClassName).ToList();
    }

    public string? GetPrimaryClass()
    {
        return _classes.FirstOrDefault()?.ClassName;
    }

    public uint GetClassLevel(string className)
    {
        return _classes.FirstOrDefault(c => c.ClassName == className)?.Level ?? 0;
    }

    public string GetSubclass(string parentName)
    {
        return _classes.FirstOrDefault(c => c.ClassName == parentName)?.SubclassName ?? string.Empty;
    }
    
    public void AddClass(string name, uint level)
    {
        if (_classes.Any(c => c.ClassName == name))
        {
            throw new ArgumentException("Character Already has a class with the given name.");
        }
        _classes.Add(new CharacterClass(name, level));
        OnPropertyChanged("Classes");
    }

    public void ChangeClassLevel(string className, uint newLevel)
    {
        var @class = _classes.FirstOrDefault(c => c.ClassName == className);
        if (@class == null)
        {
            throw new ArgumentException("Character does not have a class with the given name.");
        }

        if (newLevel < 3)
        {
            @class.SubclassName = null;
        }
        @class.Level = newLevel;
        OnPropertyChanged("Classes");
    }

    public void ChangePrimaryClass(string className)
    {
        if (!_classes.Select(c => c.ClassName).Contains(className))
        {
            throw new ArgumentException("Character does not have a class with the given name.");
        }
        var newPrimaryIndex = _classes.FindIndex(c => c.ClassName == className);
        (_classes[0], _classes[newPrimaryIndex]) = (_classes[newPrimaryIndex], _classes[0]);
    }
    
    public void RemoveClass(string className)
    {
        if (_classes.All(c => c.ClassName != className))
        {
            throw new ArgumentException("Character does not have a class with the given name.");
        }
        _classes.RemoveAll(c => c.ClassName == className);
        OnPropertyChanged("Classes");
    }

    public void AddSubclass(string parent, string name)
    {
        var @class = _classes.FirstOrDefault(c => c.ClassName == parent);
        if (@class == null)
        {
            throw new ArgumentException("Character does not have a class with the given name.");
        }
        if (@class.Level < 3)
        {
            throw new ArgumentException("Character level too low to get a subclass");
        }
        @class.SubclassName = name;
        OnPropertyChanged("Subclasses");
    }

    public void ChangeSubclass(string parent, string name)
    {
        var @class = _classes.FirstOrDefault(c => c.ClassName == parent);
        if (@class == null)
        {
            throw new ArgumentException("Character does not have a class with the given name.");
        }

        if (@class.SubclassName == null)
        {
            throw new ArgumentException("Character didn't have a subclass for the given class name before");
        }

        @class.SubclassName = name;
        OnPropertyChanged("Subclasses");
    }

    public void RemoveSubclass(string parent)
    {
        var @class = _classes.FirstOrDefault(c => c.ClassName == parent);
        if (@class == null)
        {
            throw new ArgumentException("Character does not have a class with the given name.");
        }
        @class.SubclassName = null;
        OnPropertyChanged("Subclasses");
    }
    
    public void Reset()
    {
        _classes = [];
        OnPropertyChanged("Classes");
        OnPropertyChanged("Subclasses");
    }
}