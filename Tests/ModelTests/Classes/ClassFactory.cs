using SuperDungeons.Model.Character.Classes;

namespace Tests.ModelTests.Classes;

public class ClassFactory
{
    public static ICharacterClasses GetSingleClass(string className, uint level)
    {
        var classes = new CharacterClasses();
        classes.AddClass(className, level);
        return classes;
    }
    
    public static ICharacterClasses GetDualClass(string className1, uint level1, string className2, uint level2)
    {
        if (className1 == className2) throw new ArgumentException("The same class can't be added twice");
        var classes = new CharacterClasses();
        classes.AddClass(className1, level1);
        classes.AddClass(className2, level2);
        return classes;
    }
    
    public static ICharacterClasses GetMultiClass(List<string> classNames, List<uint> levels)
    {
        var classes = new CharacterClasses();
        for (var i = 0; i < classNames.Count; i++)
        {
            classes.AddClass(classNames[i], levels[i]);
        }
        return classes;
    }
}