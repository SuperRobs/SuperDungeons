using Model.Utils;

namespace Model.Character.Classes;

public class CharacterClasses : BindableObject, ICharacterClasses
{
    //first class in this list is primary class, beyond that order is arbitrary
    //This may never be empty
    private List<CharacterClass> _classes = [];
    
    //for now this is just internal
    private readonly uint _maxlevel = 20;

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
    
    //ToDo check if class exists
    public void AddClass(string name, uint level)
    {
        if (_classes.Any(c => c.ClassName == name))
        {
            throw new ArgumentException("Character Already has a class with the given name.");
        }

        if (level == 0)
        {
            throw new ArgumentException("A Class level cannot be 0, use RemoveClass instead.");
        }
        
        if (level + _getCharacterLevelWithoutClass(name) > _maxlevel)
        {
            throw new ArgumentException("Total Character Level cannot exceed 20");
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
        
        if (newLevel == 0)
        {
            throw new ArgumentException("A Class level cannot be 0, use RemoveClass instead.");
        }
        
        if (newLevel + _getCharacterLevelWithoutClass(className) > _maxlevel)
        {
            throw new ArgumentException("Total Character Level cannot exceed 20");
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

        if (_classes.Count == 1)
        {
            throw new ArgumentException("Cannot remove last Character Class.");
        }
        _classes.RemoveAll(c => c.ClassName == className);
        OnPropertyChanged("Classes");
    }

    private uint _getCharacterLevelWithoutClass(string className)
    {
        
        //unfortunately Sum doesn't work with uint, so I need to cast it back and forth here
        return (uint) _classes.Where(c => c.ClassName != className)
            .Select(c => (int) c.Level).Sum();
    }
}