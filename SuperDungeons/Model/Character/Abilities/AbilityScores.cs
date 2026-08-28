using System.Collections.Immutable;
using SuperDungeons.Model.Rules.Features;
using SuperDungeons.Utils;

namespace SuperDungeons.Model.Character.Abilities;

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
        var highestOverride = _overrides.Where(o => o.Key.Ability==ability).Select(o => o.Value).DefaultIfEmpty().Max();
        //casting so negative values don't get Lost
        //the first ordering makes sure that bonuses are added in order of caps. Otherwise a +2 bonus with a cap of 24
        //could push the total to 20 and then a +2 with a cap of 20 would keep it at 20, however the other way around
        //it would be 22
        //the second ordering just ensures all negative values (which must always have cap 0, enforced by AddBonus)
        //are added before anything else so any possible positive bonuses are applied correctly
        var applicableBonuses = _bonuses
            .Where(b => b.Key.Ability == ability)
            .Select(b => b.Value).ToList()
            .OrderBy(b => b.Cap)
            .ThenBy(b => b.Value);
        var maxWithBonuses = (int) _baseValues[ability];
        foreach (var bonus in applicableBonuses)
        {
            //negative values are always applied, they cannot violate a cap per definition and since we gave them the
            //placeholder cap 0 this is necessary so they aren't just ignored
            if (bonus.Value < 0)
            {
                maxWithBonuses += bonus.Value;
                continue;
            }
            //this makes sure we don't accidentally replace our current maximum with the cap of some low-capped bonus
            if(maxWithBonuses < bonus.Cap)
            {
                maxWithBonuses = (int) Math.Min(maxWithBonuses + bonus.Value, bonus.Cap);
            }

        }
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

    public void AddBonus(Ability ability, FeatureIdentifier source, int bonus, uint cap = 20)
    {
        //for negative bonuses always make the cap 0, so they are evaluated first
        if (bonus < 0) cap = 0;
        AbilityBonusKey key = new(ability, source);
        _bonuses[key] = new AbilityScoreBonus(bonus, cap);
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

    // TODO add event listener to trigger death if an AS reaches 0
}