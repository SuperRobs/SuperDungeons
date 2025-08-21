using SuperDungeons.Model.Rules.Features;

namespace SuperDungeons.Model.Rules.Classes;

//ToDo move somewhere more sensible

public record SubclassData(
    string Name,
    string ParentClass,
    List<IFeature> Features);