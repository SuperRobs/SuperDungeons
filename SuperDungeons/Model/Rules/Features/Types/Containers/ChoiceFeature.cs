namespace SuperDungeons.Model.Rules.Features.Types.Containers;

public record ChoiceFeature(FeatureIdentifier Identifier, string Description, int NumberOfChoices, 
    List<IFeature> Subfeatures) : IFeature { }