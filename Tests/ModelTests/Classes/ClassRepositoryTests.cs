using SuperDungeons.Model.Rules.Classes;
using SuperDungeons.Model.Rules.DataTypes.Enums;

namespace Tests.ModelTests.Classes;

[TestFixture]
public class ClassRepositoryTests
{
    //Class

    private static readonly ClassData
        ClassData1 = new ClassData("SomeName", DiceType.D2, [], [], [], [], 0, [], [], []);

    private static readonly ClassData ClassData2 =
        new ClassData("SomeOtherName", DiceType.D2, [], [], [], [], 0, [], [], []);

    private static readonly ClassData ClassData3 =
        new ClassData("SomeDifferentName", DiceType.D2, [], [], [], [], 0, [], [], []);

    private static readonly ClassData ClassData1B =
        new ClassData("SomeName", DiceType.D4, [], [], [], [], 0, [], [], []);

    [Test]
    [Description("On Construction GetClassNames returns an empty list")]
    public void IsEmptyAtStart()
    {
        //Act
        var classRepository = new ClassRepository();
        var actual = classRepository.GetClassNames();
        //Assert
        Assert.That(actual, Is.Empty);
    }

    [Test]
    [Description("After adding a class, GetClassNames returns that classes name")]
    public void AddClass()
    {
        //Arrange
        var classRepository = new ClassRepository();
        //Act
        classRepository.AddClass(ClassData1);
        var actual = classRepository.GetClassNames();
        //Assert
        Assert.That(actual, Contains.Item(ClassData1.Name));
        Assert.That(actual, Has.Count.EqualTo(1));
    }

    [Test]
    [Description("After adding multiple classes, GetClassnames returns all those classes names")]
    public void AddMultipleClasses()
    {
        //Arrange
        var classRepository = new ClassRepository();
        //Act
        classRepository.AddClass(ClassData1);
        classRepository.AddClass(ClassData2);
        classRepository.AddClass(ClassData3);
        var actual = classRepository.GetClassNames();
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Contains.Item(ClassData1.Name));
            Assert.That(actual, Contains.Item(ClassData2.Name));
            Assert.That(actual, Contains.Item(ClassData3.Name));
            Assert.That(actual, Has.Count.EqualTo(3));
        });
    }

    [Test]
    [Description("Adding a ClassData with the same ClasName as an already existing one does nothing")]
    public void AddDuplicateClass()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        //Act
        Assert.Catch(()=>classRepository.AddClass(ClassData1B));
        var classNames = classRepository.GetClassNames();
        var savedClassData = classRepository.GetClass(ClassData1.Name);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(classNames, Contains.Item(ClassData1.Name));
            Assert.That(classNames, Has.Count.EqualTo(1));
            Assert.That(savedClassData != null && savedClassData.Equals(ClassData1));
        });
    }

    [Test]
    [Description("after adding a class, ClassExists with that classes name returns true")]
    public void ClassExistsSingleClass()
    {
        //Arrange
        var classRepository = new ClassRepository();
        //Act
        classRepository.AddClass(ClassData1);
        var actual = classRepository.ClassExists(ClassData1.Name);
        //Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    [Description("after adding multiple classes, ClassExists with one of those classes name returns true")]
    public void ClassExistsMultipleClasses()
    {
        //Arrange
        var classRepository = new ClassRepository();
        //Act
        classRepository.AddClass(ClassData1);
        classRepository.AddClass(ClassData2);
        classRepository.AddClass(ClassData3);
        var actual = classRepository.ClassExists(ClassData1.Name);
        //Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    [Description("Calling ClassExists on an empty ClassRepo returns false")]
    public void ClassNotExistsEmptyRepo()
    {
        //Arrange
        var classRepository = new ClassRepository();
        //Act
        var actual = classRepository.ClassExists("SomeClass");
        //Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    [Description("After adding a class, ClassExists with another class returns false")]
    public void ClassNotExistsWithEntryInRepo()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData2);
        //Act
        var actual = classRepository.ClassExists("SomeClass");
        //Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    [Description("GetClass will return ClassData equal to the ClassData given in at the beginning")]
    public void GetClass()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        //Act
        var actual = classRepository.GetClass(ClassData1.Name);
        //Assert
        Assert.That(actual, Is.EqualTo(ClassData1));
    }
}