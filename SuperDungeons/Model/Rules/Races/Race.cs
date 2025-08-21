using SuperDungeons.Model.Rules.Features;
using SuperDungeons.Model.Rules.Features.Types.AbilityScores;

namespace SuperDungeons.Model.Rules.Races;

public record Race(
    string Name, 
    Size Size,
    uint BaseSpeed,
    List<string> Languages,
    List<IFeature> Features,
    List<AbilityScoreBonusFeature> AbilityScoreBonuses);