using SuperDungeons.Model.Abilities;

namespace SuperDungeons.Model.Features.Types.AbilityScores;

public class AbilityScoreMinimumFeature(FeatureIdentifier identifier, string description, Ability ability, uint value,
    Abilities.AbilityScores scores) 
    : IFeature
{
    public FeatureIdentifier Identifier { get; } = identifier;
    public string Description { get; } = description;

    public void Apply()
    {
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Fixed, ability, Identifier, (int) value);
    }

    public void Remove()
    {
        scores.RemoveBonus(BonusTargets.Minimum, BonusTypes.Fixed, ability, Identifier);
    }
}