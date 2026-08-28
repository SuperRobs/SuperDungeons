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
    //Primary Class is the class the first level was taken in for multiclassing purposes
    public string? GetPrimaryClass();
    public uint GetClassLevel(string className);

    //Update Character Info
    //This component does not check whether the relevant class exists/is loaded yet
    public void AddClass(string name, uint level);
    public void ChangeClassLevel(string className, uint newLevel);
    public void ChangePrimaryClass(string className);
    public void RemoveClass(string className);
}