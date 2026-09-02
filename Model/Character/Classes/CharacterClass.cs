namespace Model.Character.Classes;

internal class CharacterClass(string className, uint level)
{
    public string ClassName { get; } = className;
    public uint Level { get; set; } = level;
}