using System.Collections.Immutable;
using SuperDungeons.Model.Character.Abilities;
using SuperDungeons.Model.Rules.DataTypes.Enums;
using SuperDungeons.Model.Rules.Features.Types.Containers;

namespace SuperDungeons.Model.Rules.Classes;


public record ClassData(
    string Name,
    DiceType HitDiceType,
    //Proficiencies
    IImmutableList<ClassLevelRestrictedFeature> ClassFeatures,
    IImmutableList<Ability> SavingThrowProficiencies,
    //ToDo everything below this is placeholder
    IImmutableSet<string> ArmorProficiencies,
    IImmutableList<string> WeaponProficiencies,
    int NumberOfSkills,
    IImmutableList<string> PossibleSkills,
    IImmutableList<string> ToolProficiencies,
    
    //this is just a placeholder, the actual starting equipment component will probably actually be a List of choices/
    //granted Items, but I don't yet know how I'll model that and I don't have the Item class yet
    IList<string> StartingEquipment);