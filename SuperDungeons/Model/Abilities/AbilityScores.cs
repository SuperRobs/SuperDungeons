using System.Diagnostics;
using SuperDungeons.Model.DataTypes.Enums;

namespace SuperDungeons.Model.Abilities;

public class AbilityScores(
    uint strength,
    uint dexterity,
    uint constitution,
    uint wisdom,
    uint intelligence,
    uint charisma)
    : BindableObject
{
    private readonly AbilityValue _scores = new(strength, dexterity, constitution, wisdom, intelligence, charisma);
    private readonly AbilityValue _maximum = new(20);
    private readonly AbilityValue _minimum = new(1);

    public uint GetAbilityScore(Ability ability)
    {
        var minimum = _minimum.GetBound(ability);
        var actualScore = _scores.GetBound(ability);
        var maximum = _maximum.GetBound(ability);
        //I'll let the minimum override the maximum if they conflict, because I think in edge cases one should rule
        //whatever is best for the players, however this should not normally happen
        return Math.Max(Math.Min(actualScore, maximum), minimum);
    }

    public int GetAbilityModifier(Ability ability)
    {
        return (int)Math.Floor(((int)GetAbilityScore(ability) - 10) / 2.0);
    }
    
    public void AddBonus(BonusTargets target, BonusTypes type, Ability ability, string source, int value)
    {
        //verify parameters
        if (!VerifyParameters(target, type, source, value)) return;
        var bonusTarget =  GetTargetedComponent(target);
        AbilityBonusKey key = new(ability, source);
        ApplyBonus(bonusTarget, key, type, value);
        //any of those may change the ability score and the score should be the only thing relevant to other classes
        OnPropertyChanged(ability.ToString());
    }

    public void RemoveBonus(BonusTargets target, BonusTypes type, Ability ability, string source)
    {
        //verify
        if (!VerifyParameters(target, type, source)) return;
        var bonusTarget =  GetTargetedComponent(target);
        AbilityBonusKey key = new(ability, source);
        RemoveBonus(bonusTarget, key, type);
        //any of those may change the ability score and the score should be the only thing relevant to other classes
        OnPropertyChanged(ability.ToString());
    }
    
    public void Reset()
    {
        _scores.Reset();
        _maximum.Reset();
        _minimum.Reset();
    }

    private static bool VerifyParameters(BonusTargets target, BonusTypes type, string source, int value)
    {
        if (!VerifyParameters(target, type, source))
        {
            return false;
        }
        
        if (type is BonusTypes.Fixed && value < 0)
        {
            Debug.WriteLine(nameof(ApplyBonus) + "with BonusType.Fixed expected positive value, but got"
                                               + value + "from source " + source);
            return false;
        }

        return true;
    }
    
    private static bool VerifyParameters(BonusTargets target, BonusTypes type, string source)
    {
        if (target is not (BonusTargets.Maximum or BonusTargets.Minimum or BonusTargets.Score))
        {
            Debug.WriteLine(nameof(AddBonus) + " expected BonusTarget Minimum, Maximum or Score, got " + target +
                            " from source " + source);
            return false;
        }
        if (type is not (BonusTypes.Change or BonusTypes.Fixed))
        {
            Debug.WriteLine(nameof(AddBonus) + " expected BonusType Fixed or Change, got " + type +
                            " from source " + source);
            return false;
        }

        return true;
    }
    
    private AbilityValue GetTargetedComponent(BonusTargets target)
    {
        var bonusTarget = target switch
        {
            BonusTargets.Maximum => _maximum,
            BonusTargets.Minimum => _minimum,
            BonusTargets.Score => _scores,
            _ => throw new ArgumentOutOfRangeException(nameof(target), target, nameof(GetTargetedComponent))
        };
        return bonusTarget;
    }

    private static void ApplyBonus(AbilityValue target, AbilityBonusKey key, BonusTypes type, int value)
    {
        switch (type)
        {
            case BonusTypes.Fixed:
                //this cast should be unproblematic, as it should have been tested beforehand that this is not negative
                //ToDo important! add test case, this is a bug waiting to happen
                target.AddFixedBound(key, (uint) value);
                break;
            case BonusTypes.Change:
                target.AddBonus(key, value);
                break;
            default:
                //there is currently no other type defined, but who knows what could happen in the future
                throw new ArgumentOutOfRangeException(nameof(type), type, nameof(RemoveBonus));
        }
    }

    //returns true if it was successfully, false if not
    private static void RemoveBonus(AbilityValue target, AbilityBonusKey key, BonusTypes type)
    {
        switch (type)
        {
            case BonusTypes.Fixed:
                target.RemoveFixedBound(key);
                break;
            case BonusTypes.Change:
                target.RemoveBonus(key);
                break;
            default:
                //there is currently no other type defined, but who knows what could happen in the future
                throw new ArgumentOutOfRangeException(nameof(type), type, nameof(RemoveBonus));
        }
    }
}