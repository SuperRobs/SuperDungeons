using System.Collections.Immutable;
using Model.Character.Abilities;
using Model.Features;
using Model.Rules.DataTypes.Enums;

namespace Model.Rules.Classes;


public record ClassData(
    string Name,
    DiceType HitDiceType,
    //Proficiencies
    IImmutableList<IFeature> ClassFeatures,
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