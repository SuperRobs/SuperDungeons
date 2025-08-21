namespace SuperDungeons.Model.Rules.Features.Types.Containers;

public record MultiFeature(FeatureIdentifier Identifier, string Description, List<IFeature> Subfeatures)
    : IFeature { }