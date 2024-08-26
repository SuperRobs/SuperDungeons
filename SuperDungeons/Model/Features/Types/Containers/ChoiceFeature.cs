namespace SuperDungeons.Model.Features.Types.Containers;

public class ChoiceFeature(
    FeatureIdentifier id,
    string description,
    int numberOfChoices,
    List<IFeature> features,
    ChoiceManager choiceManager)
    : IFeature
{
    public FeatureIdentifier Identifier { get; } = id;
    public string Description { get; } = description;
    
    // ReSharper disable once MemberCanBePrivate.Global
    public int NumberOfChoices { get; set; } = numberOfChoices;

    public List<FeatureIdentifier> GetPossibleChoices()
    {
        return features.Select(f => f.Identifier).ToList();
    }

    public void Apply()
    {
        var relevantFeatures = features
                .Where(f => choiceManager.HasChoice(Identifier, f.Identifier))
                .Take(NumberOfChoices);
        foreach(var feature in relevantFeatures) feature.Apply();
    }
    
    public void Remove()
    {
        //removes all because the ChoiceManager may have changed in the meantime, so we can't just remove those
        //that are currently chosen
        foreach (var feature in features)
        {
            feature.Remove();
        }
    }
}