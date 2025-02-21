using System.ComponentModel;
using System.Diagnostics;
using SuperDungeons.Model.Features;
using SuperDungeons.Utils;

namespace SuperDungeons.Model.Classes;

internal class CharacterClass : BindableObject
{
    private CharacterClass(ClassData characterClassData, uint level, 
        ChoiceManager choiceManager, ClassRepository repository)
    {
        _choiceManager = choiceManager;
        _choiceManager.PropertyChanged += ChoicesChanged;
        _classData = characterClassData;
        Level = level;
        _repository = repository;
    }

    private readonly ClassData _classData;
    private SubclassData? _subclass;

    public string GetClassName() => _classData.Name;
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
    private readonly ClassRepository _repository;

    public static CharacterClass Create
        (ClassData characterClassData, uint level, ChoiceManager choiceManager, ClassRepository repository)
    {
        return new CharacterClass(characterClassData, level, choiceManager, repository);
    }

    private void CheckSubclass()
    {
        var subclassIdentifier = _choiceManager
            .GetNChosenFeatures(new FeatureIdentifier("subclass", _classData.Name), 1)
            .FirstOrDefault();
        if (subclassIdentifier == null)
        {
            _subclass = null;
            return;
        }
        var subclass = _repository.GetSubclass(_classData.Name, subclassIdentifier.Title);
        if (subclass == null)
        {
            Debug.WriteLine($"Subclass {subclassIdentifier.Title} not found!");
            return;
        }
        OnPropertyChanged(_classData.Name);
        _subclass = subclass;
    }
    
    private void ChoicesChanged(object? sender, PropertyChangedEventArgs e)
    {
        CheckSubclass();
    }

    internal List<IFeature> GetClassFeatures()
    {
        List<IFeature> features = [.._classData.ClassFeatures];
        if (_subclass != null)
        {
            features.AddRange(_subclass.Features);
        }
        return features;
    }
}