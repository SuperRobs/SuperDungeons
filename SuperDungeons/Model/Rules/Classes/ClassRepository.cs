namespace SuperDungeons.Model.Rules.Classes;

public class ClassRepository
{
    private List<ClassData> Classes { get; } = [];
    
    public List<string> GetClassNames()
    {
        return Classes.Select(c => c.Name).ToList();
    }

    public bool ClassExists(string className)
    {
        return Classes.Any(c => c.Name == className);
    }

    public ClassData? GetClass(string className)
    {
        return Classes.FirstOrDefault(c => c.Name == className);
    }

    public void AddClass(ClassData @class)
    {
        if (ClassExists(@class.Name))
        {
            throw new ArgumentException("Class " + @class.Name + " already exists");
        }
        Classes.Add(@class);
    }
}