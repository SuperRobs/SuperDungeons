using System.ComponentModel;
using SuperDungeons.Model.Rules.Features;

namespace SuperDungeons.Model.Character.Abilities;

public interface IAbilityScores
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public uint GetAbilityScore(Ability ability);
    public int GetAbilityModifier(Ability ability);
     
    public void AddOverride(Ability ability, FeatureIdentifier source, uint value);
    public void AddBonus(Ability ability, FeatureIdentifier source, int bonus, uint cap = 20);
    
    public void RemoveOverride(Ability ability, FeatureIdentifier source);
    public void RemoveBonus(Ability ability, FeatureIdentifier source);
}