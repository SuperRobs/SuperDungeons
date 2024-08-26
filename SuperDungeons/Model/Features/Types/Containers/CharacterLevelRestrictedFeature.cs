using SuperDungeons.Model.Classes;

namespace SuperDungeons.Model.Features.Types.Containers;

public class CharacterLevelRestrictedFeature
    (FeatureIdentifier identifier, string description, ClassManager classManager, uint level, IFeature feature) 
    : IFeature
{
    public FeatureIdentifier Identifier { get; } = identifier;
    public string Description { get; } = description;

    public void Apply()
    {
        if (classManager.GetCharacterLevel() >= level)
        {
            feature.Apply();
        }
    }

    public void Remove()
    {
        feature.Remove();
    }
}