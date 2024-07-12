using System.Collections.Immutable;

namespace SuperDungeons.Model.Abilities;

internal class AbilityValue
{
    //having a whole dictionary of multiple overrides might be excessive, generally it'd be difficult to acquire
    //multiple setters, but it's possible and the app needs to be prepared for that
    
    //this stores fixed-value maximums (e.g. the standard 20, a hill
    //giant strength's 21 or other magic items which increase the cap)
    //the highest of those should always be used
    private readonly Dictionary<AbilityBonusKey, uint> _overrides = new();
    //this stores bonuses to the fixed-value maximums, e.g. Tomes
    //while I am not entirely sure about that, I will count them as cumulative bonuses on top of the highest maximum
    private readonly Dictionary<AbilityBonusKey, int> _modifiers = [];
    
    private readonly ImmutableDictionary<Ability, uint> _baseValues;

    public AbilityValue(uint baseValue)
    {
        _baseValues = new Dictionary<Ability, uint>
        {
            [Ability.Strength] = baseValue,
            [Ability.Dexterity] = baseValue,
            [Ability.Constitution] = baseValue,
            [Ability.Wisdom] = baseValue,
            [Ability.Intelligence] = baseValue,
            [Ability.Charisma] = baseValue
        }.ToImmutableDictionary();
    }

    public AbilityValue(uint strength, uint dexterity, uint constitution, uint wisdom, uint intelligence,
        uint charisma)
    {
        _baseValues = new Dictionary<Ability, uint>
        {
            [Ability.Strength] = strength,
            [Ability.Dexterity] = dexterity,
            [Ability.Constitution] = constitution,
            [Ability.Wisdom] = wisdom,
            [Ability.Intelligence] = intelligence,
            [Ability.Charisma] = charisma
        }.ToImmutableDictionary();
    }

    internal uint GetBound(Ability ability)
    {
        //if no Overrides are present the base value is returned
        var highestOverride = GetHighestOverride(ability);
        var modifiers = GetModifierSum(ability);
        //prevent illegal casts
        return (uint) Math.Max(highestOverride + modifiers, 0);
    }

    private uint GetHighestOverride(Ability ability)
    {
        if (_overrides.Count == 0)
        {
            return _baseValues[ability];
        }
        uint currentMax = 0;
        foreach (var keyValuePair in _overrides
                     .Where(keyValuePair => keyValuePair.Value > currentMax 
                                            && keyValuePair.Key.Ability.Equals(ability)))
        {
            currentMax = keyValuePair.Value;
        }
        return currentMax;
    }

    private int GetModifierSum(Ability ability)
    {
        return _modifiers
            .Where(modifier => modifier.Key.Ability.Equals(ability))
            .Aggregate(0, (current, keyValuePair) 
                => current + keyValuePair.Value);
    }
    
    internal void AddFixedBound(AbilityBonusKey bonusKey, uint newCap)
    {
        _overrides[bonusKey] = newCap;
    }

    internal void RemoveFixedBound(AbilityBonusKey bonusKey)
    {
        _overrides.Remove(bonusKey);
    }

    internal void AddBonus(AbilityBonusKey bonusKey, int bonus)
    {
        _modifiers[bonusKey] = bonus;
    }

    internal void RemoveBonus(AbilityBonusKey bonusKey)
    {
        _modifiers.Remove(bonusKey);
    }

    internal void Reset()
    {
        _overrides.Clear();
        _modifiers.Clear();
    }
}