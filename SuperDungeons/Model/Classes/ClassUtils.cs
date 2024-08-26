namespace SuperDungeons.Model.Classes;

public static class ClassUtils
{
    private static readonly List<Class> Classes = ClassDefinitionPersistence.LoadClasses();
    private static readonly List<Subclass> Subclasses = ClassDefinitionPersistence.LoadSubclasses();
    
    public static List<string> GetClassNames()
    {
        return Classes.Select(c => c.Name).ToList();
    }

    public static bool ClassExists(string className)
    {
        return Classes.Any(c => c.Name.Equals(className));
    }

    internal static Class? GetClass(string className)
    {
        return Classes.FirstOrDefault(c => c.Name.Equals(className));
    }
    
    public static List<string> GetSubclassNames()
    {
        return Subclasses.Select(c => c.Name).ToList();
    }

    public static bool SubClassExists(string subclassName)
    {
        return Subclasses.Any(c => c.Name.Equals(subclassName));
    }

    internal static Subclass? GetSubClass(string subclassName)
    {
        return Subclasses.FirstOrDefault(c => c.Name.Equals(subclassName));
    }

}