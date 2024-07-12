using System.Collections.Immutable;
using SuperDungeons.Model.Abilities;
using SuperDungeons.Model.Character;

namespace SuperDungeons.Model.Skills;

public class SkillManager : BindableObject
{
    private readonly IImmutableSet<Skill> _saves;
    private readonly HashSet<Skill> _skills;
    private readonly AbilityScores _scores;
    private readonly CharacterOverview _overview;

    public SkillManager(AbilityScores scores, CharacterOverview overview)
    {
        _scores = scores;
        _overview = overview;
        _saves = 
        [
            ConstructSkill("Strength SavingThrows", Ability.Strength), 
            ConstructSkill("Dexterity Saving Throws", Ability.Dexterity),
            ConstructSkill("Constitution Saving Throws", Ability.Constitution),
            ConstructSkill("Intelligence Saving Throws", Ability.Intelligence),
            ConstructSkill("Wisdom Saving Throws", Ability.Wisdom),
            ConstructSkill("Charisma Saving Throws", Ability.Charisma)
        ];
        HashSet<Skill> skills =
        [
            ConstructSkill("Acrobatics", Ability.Dexterity),
            ConstructSkill("Animal Handling", Ability.Wisdom),
            ConstructSkill("Arcana", Ability.Intelligence),
            ConstructSkill("Athletics", Ability.Strength),
            ConstructSkill("Deception", Ability.Charisma),
            ConstructSkill("History", Ability.Intelligence),
            ConstructSkill("Insight", Ability.Wisdom),
            ConstructSkill("Intimidation", Ability.Charisma),
            ConstructSkill("Investigation", Ability.Intelligence),
            ConstructSkill("Medicine", Ability.Wisdom),
            ConstructSkill("Nature", Ability.Intelligence),
            ConstructSkill("Perception", Ability.Wisdom),
            ConstructSkill("Performance", Ability.Charisma),
            ConstructSkill("Persuasion", Ability.Charisma),
            ConstructSkill("Religion", Ability.Intelligence),
            ConstructSkill("Sleight Of Hand", Ability.Dexterity),
            ConstructSkill("Stealth", Ability.Dexterity),
            ConstructSkill("Survival", Ability.Wisdom)
        ];
        _skills = skills;
    }

    public bool SkillExists(string identifier)
    {
        return _skills.Any(skill => skill.Identifier.Equals(identifier));
    }
    /// <param name="identifier"></param>
    /// <param name="ability"></param>
    /// <returns>Whether the operation was successful, false implies that the skill already exists</returns>
    public bool AddSkill(string identifier, Ability ability)
    {
        if (_skills.Any(skill => skill.Identifier.Equals(identifier)))
        {
            return false;
        }
        _skills.Add(ConstructSkill(identifier, ability));
        OnPropertyChanged("Skills");
        return true;
    }
    
    //ToDo add methods to manipulate advantage, proficiencies and bonuses

    public List<string> GetAllSaves()
    {
        return _saves.Select(save => save.Identifier).ToList();
    }

    public List<string> GetSave(Ability ability)
    {
        return _saves.Where(save => save.AssociatedAbility.Equals(ability)).Select(save => save.Identifier)
            .ToList();
    }

    public List<string> GetAllSkillIdentifiers()
    {
        return _skills.Select(skill => skill.Identifier).ToList();
    }

    public List<string> GetSkillsByAbility(Ability ability)
    {
        return _skills.Where(skill => skill.AssociatedAbility.Equals(ability)).Select(skill => skill.Identifier)
            .ToList();
    }

    public Ability? GetAbilityScore(string identifier)
    {
        return _skills.Where(skill => skill.Identifier.Equals(identifier)).Select(skill => skill.AssociatedAbility).FirstOrDefault();
    }

    /// <param name="identifier"></param>
    /// <exception cref="InvalidOperationException">when the skill does not exist</exception>
    public int GetModifier(string identifier)
    {
        return _skills.Where(skill => skill.Identifier.Equals(identifier)).Select(skill => skill.GetModifier()).First();
    }
    
    private Skill ConstructSkill(string identifier, Ability ability)
    {
        return new Skill(identifier, ability, _scores, _overview);
    }
}