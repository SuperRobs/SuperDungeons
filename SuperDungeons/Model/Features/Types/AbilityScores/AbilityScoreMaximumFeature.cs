

using SuperDungeons.Model.Character.Abilities;
using SuperDungeons.Model.Rules.Features;

namespace SuperDungeons.Model.Features.Types.AbilityScores;

public class AbilityScoreOverrideFeature(FeatureIdentifier identifier, string description, Ability ability, uint value,
    IAbilityScores scores) 
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