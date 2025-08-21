namespace SuperDungeons.Model.Rules.Features;

public interface IFeature
{
    /// <summary>
    /// The Identifier must always be unique across the entire application!
    /// </summary>
    FeatureIdentifier Identifier { get; }
    string Description { get; }
}