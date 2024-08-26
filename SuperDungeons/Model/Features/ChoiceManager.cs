using SuperDungeons.Utils;

namespace SuperDungeons.Model.Features;

public class ChoiceManager : BindableObject
{
    private readonly List<FeatureChoice> _choices = [];

    public void AddChoices(FeatureIdentifier feature, List<FeatureIdentifier> choicesToAdd)
    {
        //for now this will create duplicates
        //ToDo implement it so that there is only ever one FeatureChoice object per parent feature, this could
        //also allow for simpler implementations for the other methods, because they can assume there's only one
        _choices.Add(new FeatureChoice(feature, choicesToAdd));
    }

    public void RemoveChoices(FeatureIdentifier feature, List<FeatureIdentifier> choicesToRemove)
    {
        foreach (var choice in GetChoices(feature))
        {
            choice.Choices.RemoveAll(choicesToRemove.Contains);
        }
    }
    
    public bool HasChoice(FeatureIdentifier feature, FeatureIdentifier choice)
    {
        return GetChoices(feature).Any(c => c.Choices.Contains(choice));
    }

    public List<FeatureIdentifier> GetNChoices(FeatureIdentifier feature, int n)
    {
        return _choices
            .Where(c => c.Parent.Equals(feature))
            .SelectMany(c => c.Choices)
            .Take(n)
            .ToList();
    }
    
    private List<FeatureChoice> GetChoices(FeatureIdentifier feature)
    {
        return _choices.Where(c => c.Parent.Equals(feature)).ToList();
    }
}