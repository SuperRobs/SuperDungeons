using Model.Features;

namespace Model.Rules.Races;

public record Race(
    string Name, 
    Size Size,
    uint BaseSpeed,
    List<string> Languages,
    List<IFeature> Features,
    IFeature AbilityScoreBonuses);