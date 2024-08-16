namespace SuperDungeons.Model.Classes;

internal class Class(string className)
{
    private ClassDefinition _classDefinition = ClassDefinition.GetClassDefinition(className);
    public Subclass? subclass;
    public uint ClassLevel;
    public List<ClassFeature> features;

}