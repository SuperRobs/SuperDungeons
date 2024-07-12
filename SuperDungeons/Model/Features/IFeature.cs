namespace SuperDungeons.Model.Features;

//The term feature will describe Features and Traits from any source (Items, Classes, Races, Feats, ...)
/// <summary>
/// Features must adhere to the conditions detailed in the different method summaries!
/// </summary>
public interface IFeature
{
    string GetTitle();
    string GetDescription();
    /// <summary>
    /// Source must always be unique, or it could override (or get overridden by) other bonuses with the same name!
    /// </summary>
    string GetSource();
    /// <summary>
    /// Applies the Feature, must do nothing if called multiple times
    /// </summary>
    void Apply();
    /// <summary>
    /// Removes the Feature, must do nothing if the feature is not applied (including when being called multiple times)
    /// Must undo everything Apply() does
    /// </summary>
    void Remove();
}