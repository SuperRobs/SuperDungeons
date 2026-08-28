using SuperDungeons.Model.Character.Abilities;
using SuperDungeons.Model.Rules.Features;

namespace SuperDungeons.Model.Features.Types.AbilityScores;

public class AbilityScoreBonusFeature(FeatureIdentifier identifier, string description, Ability ability, int bonus, 
    uint cap, IAbilityScores scores) 
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