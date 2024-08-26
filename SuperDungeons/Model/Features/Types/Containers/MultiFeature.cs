namespace SuperDungeons.Model.Features.Types.Containers;

public class MultiFeature(FeatureIdentifier identifier, string description, List<IFeature> features)
    : IFeature
{
    private readonly List<IFeature> _features = [..features];
    public FeatureIdentifier Identifier { get; } = identifier;
    public string Description { get; } = description;

    public void Apply()
    {
        foreach (var feature in _features)
        {
            feature.Apply();
        }
    }

    public void Remove()
    {
        foreach (var feature in _features)
        {
            feature.Remove();
        }
    }
}