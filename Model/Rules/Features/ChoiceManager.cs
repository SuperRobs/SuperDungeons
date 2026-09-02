using Model.Utils;

namespace Model.Rules.Features;

public class ChoiceManager : BindableObject
{
    private readonly List<FeatureChoice> _choices = [];

    public void AddChoices(FeatureChoice choice)
    {
        var existingFeatureChoice = _choices
            .FirstOrDefault(c => c.Parent.Equals(choice.Parent));
        if (existingFeatureChoice != null)
        {
            _choices.Remove(existingFeatureChoice);
            _choices.Add(existingFeatureChoice 
                with { Choices = existingFeatureChoice.Choices.AddRange(choice.Choices) });
        }
        else
        {
            _choices.Add(choice);
        }
    }

    public void RemoveChoices(FeatureChoice choice)
    {
        var existingFeatureChoice = _choices
            .FirstOrDefault(c => c.Parent.Equals(choice.Parent));
        if (existingFeatureChoice is null) return; //whole featureChoice doesn't exist
        _choices.Remove(existingFeatureChoice);
        _choices.Add(existingFeatureChoice with {Choices = existingFeatureChoice.Choices
            .RemoveAll(choice.Choices.Contains)});
    }
    
    public bool HasChoices(FeatureChoice choice)
    {
        return choice.Choices.TrueForAll(c => GetChosenFeatures(choice.Parent).Contains(c));
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
}