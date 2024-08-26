using System.Collections.Immutable;
using SuperDungeons.Model.Abilities;
using SuperDungeons.Model.DataTypes.Enums;
using SuperDungeons.Model.Features;
using SuperDungeons.Model.Proficiencies;

namespace SuperDungeons.Model.Classes;

internal record Class(
    string Name,
    DiceType HitDiceType,
    //Proficiencies
    IImmutableSet<ArmorProficiencies> ArmorProficiencies,
    IImmutableList<string> WeaponProficiencies,
    int NumberOfSkills,
    IImmutableList<string> PossibleSkills,
    IImmutableList<string> ToolProficiencies,
    IImmutableList<Ability> SavingThrowProficiencies,
    
    IImmutableList<IFeature> ClassFeatures,
    IImmutableList<string> Subclasses,
    //this is just a placeholder, the actual starting equipment component will probably actually be a List of choices/
    //granted Items, but I don't yet know how I'll model that
    IList<string> StartingEquipment);