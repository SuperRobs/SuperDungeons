namespace SuperDungeons.Model.Features;

public record FeatureIdentifier(
    string Title,
    //A ClassFeature's source must be the class's identifier
    string Source);