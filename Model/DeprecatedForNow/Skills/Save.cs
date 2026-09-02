/*
using System.ComponentModel;
using SuperDungeons.Model.Abilities;
using SuperDungeons.Model.Classes;
using SuperDungeons.Model.DataTypes.Enums;
using SuperDungeons.Model.Proficiencies;

namespace SuperDungeons.Model.Skills;

//having individual skill objects allows for the user to add custom skills
//Saving Throws are also considered skills
internal class Save : BindableObject
{
    private readonly AbilityScores _scores;
    private readonly Proficiencies.Proficiencies _proficiencies;

    public Save(Ability ability, AbilityScores scores, Proficiencies.Proficiencies proficiencies)
    {
        _scores = scores;
        _proficiencies = proficiencies;
        AssociatedAbility = ability;

        _scores.PropertyChanged += ScoresChanged;
        _proficiencies.PropertyChanged += OverviewChanged;
    }
    
    private readonly string _identifier = AssociatedAbility + " Saving Throw";
    
    public Ability AssociatedAbility { get; }
    public AdvantageType Advantage { get; set; }
    public ProficiencyType Proficiency { get; set; }
    
    private readonly Dictionary<string, int> _bonuses = [];

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
        var proficiencyBonus = _proficiencies.GetProficiencyBonus();
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
            OnPropertyChanged(_identifier);
        }
    }

    private void ProficienciesChanged(object? sender, PropertyChangedEventArgs e)
    {
        // ReSharper disable once ConvertIfStatementToSwitchStatement for consistency with other method where switch is
        // not possible
        if (e.PropertyName == null) return;
        if (e.PropertyName.Equals(nameof(_proficiencies.GetProficiencyBonus)))
        {
            OnPropertyChanged(_identifier);
        }
    }
    
}
*/