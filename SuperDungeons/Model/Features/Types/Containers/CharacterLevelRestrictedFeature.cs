using System.ComponentModel;
using SuperDungeons.Model.Classes;

namespace SuperDungeons.Model.Features.Types.Containers;

public class CharacterLevelRestrictedFeature : IFeature
{
    private readonly IClassManager _classManager;
    private readonly uint _level;
    private readonly IFeature _feature;
    public FeatureIdentifier Identifier { get; }
    public string Description { get; }

    private bool _isEnabled;
    
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
        if (_classManager.GetCharacterLevel() >= _level)
        {
            _feature.Apply();
        }
        else
        {
            _feature.Remove();
        }
    }

    public CharacterLevelRestrictedFeature(FeatureIdentifier identifier, string description, IClassManager classManager, uint level, IFeature feature)
    {
        _classManager = classManager;
        _level = level;
        _feature = feature;
        Identifier = identifier;
        Description = description;

        classManager.PropertyChanged += OnClassManagerPropertiesChanged;
    }
}