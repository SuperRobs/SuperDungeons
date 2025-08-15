using SuperDungeons.Model.Abilities;

namespace SuperDungeons.Model.Features.Types.AbilityScores;

public class AbilityScoreOverrideFeature(FeatureIdentifier identifier, string description, Ability ability, uint value,
    Abilities.AbilityScores scores) 
    : IFeature
{
    public FeatureIdentifier Identifier { get; } = identifier;
    public string Description { get; } = description;

    public void Apply()
    {
        scores.AddOverride(ability, Identifier, value);
    }

    public void Remove()
    {
        scores.RemoveOverride(ability, Identifier);
    }
}