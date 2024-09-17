using SuperDungeons.Utils;

namespace SuperDungeons.Model.Features;

public class ChoiceManager : BindableObject
{
    private readonly List<FeatureChoice> _choices = [];

    public void AddChoices(FeatureIdentifier feature, List<FeatureIdentifier> choicesToAdd)
    {
        var existingChoice = _choices.FirstOrDefault(c => c.Parent.Equals(feature));
        if (existingChoice != null)
        {
            existingChoice.Choices.AddRange(choicesToAdd);
        }
        else
        {
            _choices.Add(new FeatureChoice(feature, choicesToAdd));
        }
    }

    public void RemoveChoices(FeatureIdentifier feature, List<FeatureIdentifier> choicesToRemove)
    {
        GetChoice(feature)?.Choices.RemoveAll(choicesToRemove.Contains);
    }
    
    public bool HasChoice(FeatureIdentifier feature, FeatureIdentifier choice)
    {
        return GetChosenFeatures(feature).Contains(choice);
    }

    public List<FeatureIdentifier> GetNChosenFeatures(FeatureIdentifier feature, int n)
    {
        return GetChosenFeatures(feature).Take(n).ToList();
    }
    
    private List<FeatureIdentifier> GetChosenFeatures(FeatureIdentifier feature)
    {
        return _choices
            .Where(c => c.Parent.Equals(feature))
            .SelectMany(c => c.Choices)
            .ToList();
    }

    private FeatureChoice? GetChoice(FeatureIdentifier feature)
    {
        return _choices.FirstOrDefault(c => c.Parent.Equals(feature));
    }
}