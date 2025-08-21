namespace SuperDungeons.Model.Character.Classes;

internal class CharacterClass(string className, string? subclassName, uint level)
{
    public CharacterClass(string className, uint level) : this(className, null, level) { }

    public string ClassName { get; } = className;
    public string? SubclassName { get; set; } = subclassName;
    public uint Level { get; set; } = level;
}