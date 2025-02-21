using System.ComponentModel;
namespace SuperDungeons.Model.Classes;

public interface IClassManager
{
    //events
    public event PropertyChangedEventHandler? PropertyChanged;
    
    //Get Character Info
    public uint GetCharacterProficiencyBonus();
    public uint GetCharacterLevel();
    public List<string> GetCharacterClassNames();
    public uint GetCharacterClassLevel(string className);
    public string? GetCharacterClassSubclass(string className);
    //this one combines GetCharacterClassNames/GetCharacterClassLevel/GetCharacterClassSubclass
    public HashSet<CharacterClassOverview> GetAllClassInfo();

    //Update Character Info
    public void AddClass(string name, uint level);
    public void UpdateClass(string name, uint newLevel);
    public void RemoveClass(string name);
}