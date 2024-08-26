using System.ComponentModel;
using System.Diagnostics;
using SuperDungeons.Model.Features;
using SuperDungeons.Utils;

namespace SuperDungeons.Model.Classes;

internal class ClassData : BindableObject
{
    //the same ChoiceManager contained in a classes data may be used across multiple classes
    private ClassData(Class characterClass, uint level, ChoiceManager choiceManager)
    {
        _class = characterClass;
        Level = level;
        _choiceManager = choiceManager;
        _choiceManager.PropertyChanged += ChoicesChanged;
    }

    private readonly Class _class;
    private Subclass? _subclass;

    public string GetClassName() => _class.Name;
    public string GetSubclassName() => _subclass?.Name ?? string.Empty;
    
    private uint _level;
    public uint Level
    {
        get => _level;
        set
        {
            _level = value;
            CheckSubclass();
            OnPropertyChanged(nameof(Level));
        }
    }

    private readonly ChoiceManager _choiceManager;

    public static ClassData Create(Class characterClass, uint level, ChoiceManager choiceManager)
    {
        return new ClassData(characterClass, level, choiceManager);
    }

    private void CheckSubclass()
    {
        if (Level < 3)
        {
            _subclass = null;
            return;
        }
        var subclassIdentifier = 
            _choiceManager.GetNChoices(new FeatureIdentifier("subclass", _class.Name), 1).FirstOrDefault();
        if (subclassIdentifier == null)
        {
            _subclass = null;
            return;
        }
        var subclass = ClassUtils.GetSubClass(subclassIdentifier.Title);
        if (subclass == null)
        {
            Debug.WriteLine($"Subclass {subclassIdentifier.Title} not found!");
            return;
        }

        if (_subclass is not null && _subclass.Name == subclass.Name)
        {
            return;
        }
        OnPropertyChanged(_class.Name);
        _subclass = subclass;
    }
    
    private void ChoicesChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(_class.Name);
        CheckSubclass();
    }

    public List<IFeature> GetClassFeatures()
    {
        List<IFeature> features = [.._class.ClassFeatures];
        if (_subclass != null)
        {
            features.AddRange(_subclass.Features);
        }
        return features;
    }
}