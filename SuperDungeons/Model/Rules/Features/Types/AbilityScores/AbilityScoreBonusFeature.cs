using SuperDungeons.Model.Character.Abilities;

namespace SuperDungeons.Model.Rules.Features.Types.AbilityScores;

public record AbilityScoreBonusFeature(FeatureIdentifier Identifier, string Description, Ability Ability, int Bonus, 
    uint Cap) : IFeature { }