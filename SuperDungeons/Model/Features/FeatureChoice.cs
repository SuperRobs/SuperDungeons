using System.Collections.Immutable;

namespace SuperDungeons.Model.Features;

public record FeatureChoice(
    //the Identifier references the ChoiceFeature which contains the Subfeatures
    FeatureIdentifier Parent,
    //the amount of possible choices depends on the Feature, but can not be validated here. If too many choices are made
    //only the first ones will be used
    //these Identifiers reference the chosen Subfeatures
    ImmutableList<FeatureIdentifier> Choices);