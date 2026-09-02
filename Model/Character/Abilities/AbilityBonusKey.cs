using Model.Rules.Features;

namespace Model.Character.Abilities;

internal record AbilityBonusKey(Ability Ability, FeatureIdentifier Source);