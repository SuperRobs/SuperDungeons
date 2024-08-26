using SuperDungeons.Model.Abilities;

namespace SuperDungeons.Model.Features.Types.AbilityScores;

public class AbilityScoreBonusFeature(FeatureIdentifier identifier, string description, Ability ability, int bonus,
    Abilities.AbilityScores scores) 
    : IFeature
{
    public FeatureIdentifier Identifier { get; } = identifier;
    public string Description { get; } = description;

    public void Apply()
    {
        scores.AddBonus(BonusTargets.Score, BonusTypes.Change, ability, Identifier, bonus);
    }

    public void Remove()
    {
        scores.RemoveBonus(BonusTargets.Score, BonusTypes.Change, ability, Identifier);
    }
}