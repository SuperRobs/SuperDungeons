using System.Collections.Immutable;
using SuperDungeons.Model.Features;
using SuperDungeons.Utils;

namespace SuperDungeons.Model.Abilities;

public class AbilityScores(
    uint strength,
    uint dexterity,
    uint constitution,
    uint wisdom,
    uint intelligence,
    uint charisma)
    : BindableObject, IAbilityScores
{
    //having a whole dictionary of multiple overrides might be excessive, generally it'd be difficult to acquire
    //multiple setters, but it's possible and the app needs to be prepared for that.
    
    //this stores fixed-value buffs (e.g. a hill giant strength 21 or other magic items)
    //the highest of those should always be used
    private readonly Dictionary<AbilityBonusKey, uint> _overrides = new();
    //this stores bonuses to the ability scores, e.g. Tomes
    //as far as my rules-understanding goes, these do not stack on overrides
    private readonly Dictionary<AbilityBonusKey, AbilityScoreBonus> _bonuses = [];
    
    private readonly ImmutableDictionary<Ability, uint> _baseValues = new Dictionary<Ability, uint>
    {
        [Ability.Strength] = strength,
        [Ability.Dexterity] = dexterity,
        [Ability.Constitution] = constitution,
        [Ability.Wisdom] = wisdom,
        [Ability.Intelligence] = intelligence,
        [Ability.Charisma] = charisma
    }.ToImmutableDictionary();

    public uint GetAbilityScore(Ability ability)
    {
        //ToDo I'm very sure there are edge cases here that I haven't tested yet, especially regarding negative values
        var highestOverride = _overrides.Values.Max();
        //casting so negative values don't get Lost
        //the first ordering makes sure that bonuses are added in order of caps. Otherwise a +2 bonus with a cap of 24
        //could push the total to 20 and then a +2 with a cap of 20 would keep it at 20, however the other way around
        //it would be 22
        //the second ordering just ensures all negative values (which must always have cap 0, enforced by AddBonus)
        //are added before anything else for similarly unlikely cases
        var maxWithBonuses = _bonuses.Values.ToList()
            .OrderBy(b => b.Cap)
            .ThenBy(b => b.Value)
            .Aggregate((int)_baseValues[ability], (current, bonus) => (int)Math.Min(current + bonus.Value, bonus.Cap));
        var totalMax = Math.Max(maxWithBonuses, highestOverride);
        //prevent issues if total score would be negative
        return (uint) Math.Max(totalMax, 0);
    }
    
    public int GetAbilityModifier(Ability ability)
    {
        return (int)Math.Floor(((int)GetAbilityScore(ability) - 10) / 2.0);
    }


    public void AddOverride(Ability ability, FeatureIdentifier source, uint value)
    {
        AbilityBonusKey key = new(ability, source);
        _overrides[key] = value;
        OnPropertyChanged(ability.ToString());
    }

    public void AddBonus(Ability ability, FeatureIdentifier source, AbilityScoreBonus bonus)
    {
        //for negative bonuses always make the cap 0, so they are evaluated first
        if (bonus.Value < 0) bonus = 
            bonus with { Cap = 0 };
        AbilityBonusKey key = new(ability, source);
        _bonuses[key] = bonus;
        OnPropertyChanged(ability.ToString());
    }

    public void RemoveOverride(Ability ability, FeatureIdentifier source)
    {
        AbilityBonusKey key = new(ability, source);
        _overrides.Remove(key);
        OnPropertyChanged(ability.ToString());
    }

    public void RemoveBonus(Ability ability, FeatureIdentifier source)
    {
        AbilityBonusKey key = new(ability, source);
        _bonuses.Remove(key);
        OnPropertyChanged(ability.ToString());
    }
}