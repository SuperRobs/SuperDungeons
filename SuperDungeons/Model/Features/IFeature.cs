namespace SuperDungeons.Model.Features;

public interface IFeature
{
    /// <summary>
    /// The Identifier must always be unique across the entire application!
    /// </summary>
    FeatureIdentifier Identifier { get; }
    string Description { get; }
    void Apply();
    /// <summary>
    /// Remove must do nothing if the feature is not applied!
    /// </summary>
    void Remove();
}