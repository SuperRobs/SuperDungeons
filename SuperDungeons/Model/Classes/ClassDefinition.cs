using System.Collections.Immutable;
using SuperDungeons.Model.DataTypes.Enums;
using SuperDungeons.Model.Features;

namespace SuperDungeons.Model.Classes;

internal record ClassDefinition(
    string Name,
    DiceType HitDiceType,
    IImmutableSet<string> Proficiencies,
    IImmutableSet<string> SkillProficiencies,
    IList<string> StartingEquipment,
    IImmutableSet<IFeature> Features)
    //still needs some component for spell slots/other resources
{
    public static ClassDefinition GetClassDefinition(string className)
    {
        //ToDo actual implementation
        return new ClassDefinition("", DiceType.D10, [], [], [], []);
    }

    public static bool ClassDefinitionExists()
    {
        return true;
    }

    internal static void Save()
    {
        //ToDo
    }
}