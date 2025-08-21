namespace SuperDungeons.Model.Rules.Features;

public record FeatureIdentifier(
    //A title should be a feature name or a variant of it (f.e. ASI 4 STR 2, ASI 8 DEX 1 for an ASI at level 4,
    //one of 2 STR points and 1 DEX point
    string Title,
    //A ClassFeature's source should be the class Identifier, a Feat feature's source should be the feat name...
    string Source);