using System.ComponentModel;

namespace SuperDungeons.Model.Character.Classes;

public interface ICharacterClasses
{
    //events
    public event PropertyChangedEventHandler? PropertyChanged;
    
    //Get Character Info
    public uint GetProficiencyBonus();
    public uint GetCharacterLevel();
    public List<string> GetClassNames();
    public string? GetPrimaryClass();
    public uint GetClassLevel(string className);
    public string? GetSubclass(string className);

    //Update Character Info
    public void AddClass(string name, uint level);
    public void ChangeClassLevel(string className, uint newLevel);
    public void ChangePrimaryClass(string className);
    public void RemoveClass(string className);
    
    public void AddSubclass(string parent, string name);
    public void RemoveSubclass(string parent);

    public void Reset();
}