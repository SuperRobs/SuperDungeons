namespace SuperDungeons.Model.Rules.Features.Types.Containers;

public record ClassLevelRestrictedFeature(FeatureIdentifier Identifier, string Description, uint Level, 
        string ClassIdentifier, IFeature Subfeature) : IFeature { }