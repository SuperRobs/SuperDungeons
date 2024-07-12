namespace SuperDungeons.Model.Classes;

public class Class(string className)
{
    private ClassDefinition _classDefinition = ClassDefinition.GetClassDefinition(className);
    public uint ClassLevel;
    
    
}