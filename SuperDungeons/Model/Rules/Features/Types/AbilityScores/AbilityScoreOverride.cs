using SuperDungeons.Model.Character.Abilities;

namespace SuperDungeons.Model.Rules.Features.Types.AbilityScores;

public record AbilityScoreOverrideFeature(FeatureIdentifier Identifier, string Description, Ability Ability, uint Value) 
    : IFeature { }