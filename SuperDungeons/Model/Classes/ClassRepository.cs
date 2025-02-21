using System.Windows.Input;

namespace SuperDungeons.Model.Classes;

public class ClassRepository
{
    private List<ClassData> Classes { get; } = [];
    private Dictionary<string, List<SubclassData>> Subclasses { get; } = [];


    public List<string> GetClassNames()
    {
        return Classes.Select(c => c.Name).ToList();
    }

    public List<string> GetSubclassNames(string className)
    {
        return Subclasses.Where(c => c.Key.Equals(className))
            .SelectMany(c => c.Value)
            .Select(c => c.Name)
            .ToList();
    }

    public bool ClassExists(string className)
    {
        return Classes.Any(c => c.Name == className);
    }

    public bool SubclassExists(string className, string subClassName)
    {
        return Subclasses.ContainsKey(className) && Subclasses[className].Any(c => c.Name == subClassName);
    }

    public ClassData? GetClass(string className)
    {
        return Classes.FirstOrDefault(c => c.Name == className);
    }

    public SubclassData? GetSubclass(string className, string subclassName)
    {
        return Subclasses[className].FirstOrDefault(c => c.Name == subclassName);
    }

    public void AddClass(ClassData @class)
    {
        //first remove any possible duplicates
        if (ClassExists(@class.Name))
        {
            Classes.RemoveAll(c => c.Name.Equals(@class.Name));
        }
        Classes.Add(@class);
    }

    public void AddSubclass(SubclassData subclassData)
    {
        var parent = GetClass(subclassData.ParentClass);
        if (parent == null) return;
        var appropriateList = Subclasses
            .FirstOrDefault(c => c.Key.Equals(subclassData.ParentClass)).Value;
        if (appropriateList is null)
        {
            appropriateList = [];
            Subclasses.Add(subclassData.ParentClass, appropriateList);
        }
        //classes are already guaranteed to match, so this will remove any duplicates
        //if this was null it would have been added in the above if, but Rider wants to have it double-checked
        appropriateList.RemoveAll(c => c.Name.Equals(subclassData.Name));
        appropriateList.Add(subclassData);
    }
    
    //Delete Capabilities?
}