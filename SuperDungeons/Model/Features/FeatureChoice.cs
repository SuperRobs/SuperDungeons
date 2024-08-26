namespace SuperDungeons.Model.Features;

internal class FeatureChoice(
    //the Identifier references the ChoiceFeature which contains the Subfeatures
    FeatureIdentifier parent,
    //the amount of possible choices depends on the Feature, but can not be validated here. If too many choices are made
    //only the first ones will be used
    //these Identifiers reference the chosen Subfeatures
    List<FeatureIdentifier> choices)
{
    public FeatureIdentifier Parent { get; } = parent;
    public List<FeatureIdentifier> Choices { get; } = choices;
}