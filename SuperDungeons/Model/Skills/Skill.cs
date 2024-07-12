using System.ComponentModel;
using SuperDungeons.Model.Abilities;
using SuperDungeons.Model.Character;
using SuperDungeons.Model.DataTypes.Enums;

namespace SuperDungeons.Model.Skills;

//having individual skill objects allows for the user to add custom skills
//Saving Throws are also considered skills
public class Skill : BindableObject
{
    private readonly AbilityScores _scores;
    private readonly CharacterOverview _overview;

    public Skill(string identifier, Ability ability, AbilityScores scores, CharacterOverview overview)
    {
        _scores = scores;
        _overview = overview;
        Identifier = identifier;
        AssociatedAbility = ability;

        _scores.PropertyChanged += ScoresChanged;
        _overview.PropertyChanged += OverviewChanged;
    }

    public string Identifier { get; }
    // ReSharper disable once MemberCanBePrivate.Global
    public Ability AssociatedAbility { get; }
    public AdvantageType Advantage { get; set; }
    public ProficiencyType Proficiency { get; set; }
    
    private Dictionary<string, int> _bonuses = [];

    public void AddBonus(string source, int bonus)
    {
        _bonuses.Add(source, bonus);
    }

    public void RemoveBonus(string source)
    {
        _bonuses.Remove(source);
    }
    
    public int GetModifier()
    {
        var abilityModifier = _scores.GetAbilityModifier(AssociatedAbility);
        var proficiencyBonus = _overview.GetProficiencyBonus();
        switch (Proficiency)
        {
            case ProficiencyType.None:
                proficiencyBonus = 0;
                break;
            case ProficiencyType.Half:
                proficiencyBonus /= 2;
                break;
            case ProficiencyType.Full:
                proficiencyBonus *= 1;
                break;
            case ProficiencyType.Expertise:
                proficiencyBonus *= 2;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GetModifier) + " expected Proficicy to be" +
                                                      "None, Half, Full or Expertise, but was " + Proficiency);
        }
        var otherBonuses = _bonuses.Aggregate(0, (sum, pair) => sum + pair.Value);
        //they shouldn't get big enough for this cast to be a problem
        return (int)(abilityModifier + proficiencyBonus + otherBonuses);
    }

    private void ScoresChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == null) return;
        if (e.PropertyName.Equals(AssociatedAbility.ToString()))
        {
            OnPropertyChanged(Identifier);
        }
    }

    private void OverviewChanged(object? sender, PropertyChangedEventArgs e)
    {
        // ReSharper disable once ConvertIfStatementToSwitchStatement for consistency with other method where switch is
        // not possible
        if (e.PropertyName == null) return;
        if (e.PropertyName.Equals(nameof(_overview.GetProficiencyBonus)))
        {
            OnPropertyChanged(Identifier);
        }
    }
}