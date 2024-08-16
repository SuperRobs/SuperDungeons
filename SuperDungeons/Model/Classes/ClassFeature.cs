using System.Collections.Immutable;
using SuperDungeons.Model.Features;

namespace SuperDungeons.Model.Classes;

internal class ClassFeature
{
    private ClassFeature(string name, string shortDescription, string longDescription, uint level,
        IImmutableDictionary<string, IFeature> choices)
    {
        Name = name;
        ShortDescription = shortDescription;
        LongDescription = longDescription;
        UnlockLevel = level;
        Choices = choices;
    }
    //name will also be the unique identifier of a feature, so they can be used to reference specific features,
    //e.g. when saving decisions
    /// <summary>
    /// Name must always be unique inside a class!
    /// </summary>
    public string Name { get; }
    public string ShortDescription { get; }
    public string LongDescription { get; }
    
    public uint UnlockLevel { get; }
    public readonly IImmutableDictionary<string, IFeature> Choices;

    public static ClassFeature BuildFeature(string stringRepresentation)
    {
        //ToDo
        return new ClassFeature("", "", "", 0, 
            new Dictionary<string, IFeature>().ToImmutableDictionary());
    }
}