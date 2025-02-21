using SuperDungeons.Model.Features;

namespace SuperDungeons.Model.Classes;

public record SubclassData(
    string Name,
    string ParentClass,
    List<IFeature> Features);