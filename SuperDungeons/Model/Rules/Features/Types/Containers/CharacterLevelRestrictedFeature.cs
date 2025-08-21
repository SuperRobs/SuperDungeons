namespace SuperDungeons.Model.Rules.Features.Types.Containers;

public record CharacterLevelRestrictedFeature(FeatureIdentifier Identifier, string Description, uint Level, 
    IFeature Subfeature) : IFeature { }