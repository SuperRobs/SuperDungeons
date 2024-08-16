using System.Collections.Immutable;
using SuperDungeons.Model.DataTypes.Enums;

namespace SuperDungeons.Model.Classes;

internal record ClassDefinition(
    string Name,
    DiceType HitDiceType,
    IImmutableSet<object> ProficencyplaceHolder,
    //IImmutableSet<Proficiency> Proficiencies;
    IImmutableSet<ClassFeature> FeatureIdentifiers,
    IImmutableSet<Subclass> SubClassIdentifiers)
{
    public static ClassDefinition GetClassDefinition(string className)
    {
        //ToDo actual implementation
        return new ClassDefinition("", DiceType.D10, [], [], []);
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