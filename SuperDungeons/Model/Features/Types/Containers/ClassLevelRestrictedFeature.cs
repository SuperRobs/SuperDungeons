using System.ComponentModel;
using SuperDungeons.Model.Classes;

namespace SuperDungeons.Model.Features.Types.Containers;

public class ClassLevelRestrictedFeature
    //The identifier's source must be the name of the class
    : IFeature
{
    public FeatureIdentifier Identifier { get; }
    public string Description { get; }
    private bool _isEnabled;
    private readonly IClassManager _classManager;
    private readonly string _classIdentifier;
    private readonly uint _level;
    private readonly IFeature _feature;

    public ClassLevelRestrictedFeature(FeatureIdentifier identifier, string description, IClassManager classManager, string classIdentifier, uint level, 
        IFeature feature)
    {
        _classManager = classManager;
        _classIdentifier = classIdentifier;
        _level = level;
        _feature = feature;
        Identifier = identifier;
        Description = description;

        _classManager.PropertyChanged += OnClassManagerPropertiesChanged;
    }

    public void Apply()
    {
        _isEnabled = true;
        LevelDecision();
    }

    public void Remove()
    {
        _isEnabled = false;
        _feature.Remove();
    }
    
    private void OnClassManagerPropertiesChanged(object? sender, PropertyChangedEventArgs propertyChangedEventArgs)
    {
        if (propertyChangedEventArgs.PropertyName is not "Level")
        {
            return;
        }
        if (!_isEnabled) return;
        //this will lead to features being regularly applied multiple times, this is intended, this is why
        //features apply and remove features must be idempotent
        LevelDecision();
    }

    private void LevelDecision()
    {
        if (_classManager.GetCharacterClassLevel(_classIdentifier) >= _level)
        {
            _feature.Apply();
        }
        else
        {
            _feature.Remove();
        }
    }
}