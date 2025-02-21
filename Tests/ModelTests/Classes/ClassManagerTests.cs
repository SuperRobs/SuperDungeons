using SuperDungeons.Model.Classes;
using SuperDungeons.Model.DataTypes.Enums;
using SuperDungeons.Model.Features;

namespace Tests.ModelTests.Classes;

public class ClassManagerTests
{
    //Assumptions: ClassRepository works correctly and is not edited by the ClassManager
    private readonly ClassRepository _classRepository = SetupClassRepository();
    //The ClassManager is not responsible for managing the proficiencies/skills/equipment,
    //so I will just leave those empty here
    private static readonly ClassData Class1 = new("Class1", DiceType.D2, [], [], 0, [], [], [], [], []);
    private static readonly ClassData Class2 = new("Class2", DiceType.D2, [], [], 0, [], [], [], [], []);
    private static readonly SubclassData Subclass1ForClass1 = new("Subclass1ForClass1", "Class1", []);
    private static readonly SubclassData Subclass2ForClass1 = new("Subclass2ForClass1", "Class1", []);
    private static readonly SubclassData Subclass1ForClass2 = new("Subclass1ForClass2", "Class1", []);
    private static readonly SubclassData Subclass2ForClass2 = new("Subclass2ForClass2", "Class1", []);
    //The choiceManager must only be used to check for subclasses,
    //so this empty one should suffice for all tests that don't examine subclasses
    private readonly ChoiceManager _emptyChoiceManager = new();
    private static ClassRepository SetupClassRepository()
    {
        ClassRepository repo = new();
        repo.AddClass(Class1);
        repo.AddClass(Class2);
        repo.AddSubclass(Subclass1ForClass1);
        repo.AddSubclass(Subclass2ForClass1);
        repo.AddSubclass(Subclass1ForClass2);
        repo.AddSubclass(Subclass2ForClass2);
        return repo;
    }
    
    //Manage Classes
    //AddClass/GetCharacterClassNames 
    [Test]
    [Description("On a newly created ClassManager, GetCharacterClassNames returns an empty list")]
    public void EmptyGetClassNames()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        //Act
        var actual = mgr.GetCharacterClassNames();
        //Assert
        Assert.That(actual, Is.Empty);
    }

    [Test]
    [Description("After adding a Class, GetCharacterClassNames contains exactly the name of that class")]
    public void OneClassGetClassNames()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        //Act
        mgr.AddClass(Class1.Name, 1);
        var actual = mgr.GetCharacterClassNames();
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(1));
            Assert.That(actual, Does.Contain(Class1.Name));
        });
    }

    [Test]
    [Description("After adding two Classes, GetCharacterClassNames contains exactly those two classes' names")]
    public void TwoClassGetClassNames()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        //Act
        mgr.AddClass(Class1.Name, 1);
        mgr.AddClass(Class2.Name, 1);
        var actual = mgr.GetCharacterClassNames();
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(2));
            Assert.That(actual, Does.Contain(Class1.Name));
            Assert.That(actual, Does.Contain(Class2.Name));
        });
    }

    [Test]
    [Description("After adding the same Class twice, GetCharacterClassNames contains only one element")]
    public void AddSameClassTwice()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        //Act
        mgr.AddClass(Class1.Name, 1);
        mgr.AddClass(Class1.Name, 1);
        var actual = mgr.GetCharacterClassNames();
        //Assert
        Assert.That(actual, Has.Count.EqualTo(1));
    }
    
    [Test]
    [Description("After adding a class that's not in the ClassRepository, GetCharacterClassNames is still empty")]
    public void AddNonExistingClass()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        //Act
        mgr.AddClass("urgfaerfiuyeaey", 1);
        var actual = mgr.GetCharacterClassNames();
        //Assert
        Assert.That(actual, Has.Count.EqualTo(0));
    }
    
    //UpdateClass (assumes GetCharacterClassLevel works correctly) ToDo 
    //RemoveClass ToDo 
    
    //Get Info
    //GetCharacterClassLevel
    [Test]
    [Description("On a new ClassManager, GetCharacterClassLevel returns 0 for an arbitrary name")]
    public void EmptyManagerGetCharacterClassLevel()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        //Act
        var actual = mgr.GetCharacterClassLevel("some name");
        //Assert
        Assert.That(actual, Is.EqualTo(0));
    }
    
    [Test]
    [Description("After adding a class, GetCharacterClassLevel for that class returns the correct value")]
    public void SingleClassGetCharacterClassLevel()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        mgr.AddClass(Class1.Name, 1);
        //Act
        var actual = mgr.GetCharacterClassLevel(Class1.Name);
        //Assert
        Assert.That(actual, Is.EqualTo(1));
    }
    
    [Test]
    [Description("After adding a class, GetCharacterClassLevel for another arbitrary name returns the correct value")]
    public void SingleClassGetOtherCharacterClassLevel()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        mgr.AddClass(Class1.Name, 1);
        //Act
        var actual = mgr.GetCharacterClassLevel("some name");
        //Assert
        Assert.That(actual, Is.EqualTo(0));
    }

    [Test]
    [Description("After adding multiple classes, GetCharacterClassLevel for one of those returns the correct value")]
    public void MultipleClassesGetCharacterClassLevel()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        mgr.AddClass(Class1.Name, 2);
        mgr.AddClass(Class2.Name, 3);
        //Act
        var actual = mgr.GetCharacterClassLevel(Class1.Name);
        //Assert
        Assert.That(actual, Is.EqualTo(2));
    }
    
    //GetCharacterClassSubclass ToDo
    //GetCharacterLevel
    [Test]
    [Description("On an empty ClassManager, GetCharacterLevel returns 0")]
    public void EmptyManagerGetCharacterLevel()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        //Act
        var actual = mgr.GetCharacterLevel();
        //Assert
        Assert.That(actual, Is.EqualTo(0));
    }

    [Test]
    [Description("After adding a class GetCharacterLevel returns that classes level")]
    public void SingleClassGetCharacterLevel()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        mgr.AddClass(Class1.Name, 1);
        //Act
        var actual = mgr.GetCharacterLevel();
        //Assert
        Assert.That(actual, Is.EqualTo(1));
    }
    
    [Test]
    [Description("After adding two classes GetCharacterLevel returns the sum of those classes' level")]
    public void TwoClassesGetCharacterLevel()
    {
        //Arrange
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        mgr.AddClass(Class1.Name, 1);
        mgr.AddClass(Class2.Name, 1);
        //Act
        var actual = mgr.GetCharacterLevel();
        //Assert
        Assert.That(actual, Is.EqualTo(2));
    }
    //GetCharacterProficiencyBonus ToDo 

    [Test]
    [Description("using GetCharacterProficiencyBonus on a level 1 character returns 2")]
    public void GetProfLevel1()
    {
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        mgr.AddClass(Class1.Name, 1);
        var actual = mgr.GetCharacterProficiencyBonus();
        Assert.That(actual, Is.EqualTo(2));
    }
    
    [Test]
    [Description("using GetCharacterProficiencyBonus on a level 20 character returns 6")]
    public void GetProfLevel20()
    {
        ClassManager mgr = new(_classRepository, _emptyChoiceManager);
        mgr.AddClass(Class1.Name, 20);
        var actual = mgr.GetCharacterProficiencyBonus();
        Assert.That(actual, Is.EqualTo(6));
    }
    
    
}