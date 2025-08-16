using SuperDungeons.Model.Abilities;

namespace SuperDungeons.Model.Features.Types.AbilityScores;

public class AbilityScoreBonusFeature(FeatureIdentifier identifier, string description, Ability ability, int bonus, 
    uint cap, Abilities.AbilityScores scores) 
    : IFeature
{
    public FeatureIdentifier Identifier { get; } = identifier;
    public string Description { get; } = description;

    public void Apply()
    {
        scores.AddBonus(ability, Identifier, bonus, cap);
    }

    public void Remove()
    {
        scores.RemoveBonus(ability, Identifier);
    }
}