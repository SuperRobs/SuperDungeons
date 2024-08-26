using SuperDungeons.Model.Features;

namespace SuperDungeons.Model.Classes;

public record Subclass(
    string Name,
    string ParentClass,
    List<IFeature> Features
    );