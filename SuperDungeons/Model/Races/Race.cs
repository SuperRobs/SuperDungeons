using SuperDungeons.Model.Features;
using SuperDungeons.Model.Features.Types.AbilityScores;

namespace SuperDungeons.Model.Races;

public record Race(
    string Name, 
    Size Size,
    uint BaseSpeed,
    List<string> Languages,
    List<IFeature> Features,
    List<AbilityScoreBonusFeature> AbilityScoreBonuses);