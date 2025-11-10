using SuperDungeons.Model.Abilities;
using SuperDungeons.Model.Classes;
using SuperDungeons.Model.DataTypes.Enums;
using SuperDungeons.Model.Features;
using SuperDungeons.Model.Features.Types.AbilityScores;
using Tests.ModelTests.Abilities;

namespace Tests.ModelTests.Classes;

[TestFixture]
public class ClassRepositoryTests
{
    //Class

    private static readonly ClassData
        ClassData1 = new ClassData("SomeName", DiceType.D2, [], [], 0, [], [], [], [], []);

    private static readonly ClassData ClassData2 =
        new ClassData("SomeOtherName", DiceType.D2, [], [], 0, [], [], [], [], []);

    private static readonly ClassData ClassData3 =
        new ClassData("SomeDifferentName", DiceType.D2, [], [], 0, [], [], [], [], []);

    private static readonly ClassData ClassData1B =
        new ClassData("SomeName", DiceType.D4, [], [], 1, [], [], [], [], []);

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
        classRepository.AddClass(ClassData1B);
        var classNames = classRepository.GetClassNames();
        var savedClassData = classRepository.GetClass(ClassData1.Name);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(classNames, Contains.Item(ClassData1.Name));
            Assert.That(classNames, Has.Count.EqualTo(1));
            Assert.That(savedClassData != null && savedClassData.Equals(ClassData1B));
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

    //Subclass

    private static readonly SubclassData Subclass1ForClass1 = new SubclassData("SomeNameSub", ClassData1.Name, []);

    private static readonly SubclassData Subclass2ForClass1 =
        new SubclassData("SomeNameDifferentSub", ClassData1.Name, []);
    private static readonly SubclassData Subclass1ForClass2 = new SubclassData("SomeOtherNameSub", ClassData2.Name, []);
    private static readonly SubclassData Subclass2ForClass2 = new SubclassData("SomeNameSub", ClassData2.Name, []);
    private static readonly SubclassData Subclass3ForClass2 =
        new SubclassData("SomeOtherNameEvenDifferentSub", ClassData2.Name, []);

    //the feature is just so that they are not equal
    private static readonly SubclassData Subclass1BForClass1 = new SubclassData("SomeNameSub", ClassData1.Name,
        [new AbilityScoreBonusFeature(new FeatureIdentifier("", ""), "", Ability.Charisma, 0, 20, AbilityScoreFactory.AllZero())]);

    [Test]
    [Description("On Construction GetSubClassNames for an arbitrary class name returns an empty list")]
    public void SubIsEmptyAtStart()
    {
        //Act
        var classRepository = new ClassRepository();
        var actual = classRepository.GetSubclassNames(ClassData1.Name);
        //Assert
        Assert.That(actual, Is.Empty);
    }

    [Test]
    [Description("Trying to add a subclass where the parent class does not exist does nothing")]
    public void SubClassToNonExistentParent()
    {
        //Arrange
        var classRepository = new ClassRepository();
        //Act
        classRepository.AddSubclass(Subclass1ForClass1);
        var actual = classRepository.GetSubclassNames(ClassData1.Name);
        //Assert
        Assert.That(actual, Is.Empty);
    }

    [Test]
    [Description("After adding a subclass to a valid class, it can be found via GetSubclassNames")]
    public void AddValidSubclass()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        //Act
        classRepository.AddSubclass(Subclass1ForClass1);
        var actual = classRepository.GetSubclassNames(ClassData1.Name);
        //Assert
        Assert.That(actual, Has.Count.EqualTo(1));
        Assert.That(actual, Contains.Item(Subclass1ForClass1.Name));
    }

    [Test]
    [Description("Adding multiple subclasses to a single class")]
    public void AddMultipleSubclassesForClass()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        //Act
        classRepository.AddSubclass(Subclass1ForClass1);
        classRepository.AddSubclass(Subclass2ForClass1);
        var actual = classRepository.GetSubclassNames(ClassData1.Name);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Does.Contain(Subclass1ForClass1.Name));
            Assert.That(actual, Does.Contain(Subclass2ForClass1.Name));
            Assert.That(actual, Has.Count.EqualTo(2));
        });
    }

    [Test]
    [Description("Adding multiple subclasses for separate classes")]
    public void AddMultipleSubclassesForSeparateClasses()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        classRepository.AddClass(ClassData2);
        //Act
        classRepository.AddSubclass(Subclass1ForClass1);
        classRepository.AddSubclass(Subclass1ForClass2);
        var actual1 = classRepository.GetSubclassNames(ClassData1.Name);
        var actual2 = classRepository.GetSubclassNames(ClassData2.Name);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.Multiple(() =>
            {
                Assert.That(actual1, Has.Count.EqualTo(1));
                Assert.That(actual1, Does.Contain(Subclass1ForClass1.Name));
            });
            Assert.Multiple(() =>
            {
                Assert.That(actual2, Has.Count.EqualTo(1));
                Assert.That(actual2, Does.Contain(Subclass1ForClass2.Name));
            });
        });
    }

    [Test]
    [Description("Adding duplicate subclass (same name, same parent, " +
                 "different features) discards old, keeps new subclass")]
    public void AddDuplicateSubclass()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        //Act
        classRepository.AddSubclass(Subclass1ForClass1);
        classRepository.AddSubclass(Subclass1BForClass1);
        var actual = classRepository.GetSubclassNames(ClassData1.Name);
        //Assert
        Assert.That(actual, Has.Count.EqualTo(1));
        Assert.That(actual, Contains.Item(Subclass1BForClass1.Name));
    }

    //I'm not exactly sure why one would do that, but I don't see a point in restricting it
    [Test]
    [Description("Adding multiple subclasses with the same name for different classes works")]
    public void AddDuplicateDifferentParents()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        classRepository.AddClass(ClassData2);
        //Act
        classRepository.AddSubclass(Subclass1ForClass1);
        classRepository.AddSubclass(Subclass2ForClass2);
        var actual1 = classRepository.GetSubclassNames(ClassData1.Name);
        var actual2 = classRepository.GetSubclassNames(ClassData2.Name);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual1, Has.Count.EqualTo(1));
            Assert.That(actual2, Has.Count.EqualTo(1));
            Assert.That(actual1, Contains.Item(Subclass1ForClass1.Name));
            Assert.That(actual2, Contains.Item(Subclass2ForClass2.Name));
        });
    }

    [Test]
    [Description("SubclassExists with single subclass")]
    public void SubclassExistsWithSingleSubclass()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        classRepository.AddSubclass(Subclass1ForClass1);
        //Act
        var actual = classRepository.SubclassExists(ClassData1.Name, Subclass1ForClass1.Name);
        //Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    [Description("SubclassExists with multiple subclasses in multiple classes (different names)")]
    public void SubclassExistsWithMultipleSubclassesInMultipleClasses()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        classRepository.AddClass(ClassData2);
        classRepository.AddSubclass(Subclass1ForClass1);
        classRepository.AddSubclass(Subclass3ForClass2);
        //Act
        var actual = classRepository.SubclassExists(ClassData1.Name, Subclass1ForClass1.Name);
        //Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    [Description("On a fresh ClassRepo SubclassExists returns false for an arbitrary class and subclass name")]
    public void SubclassNotExistsWithEmptyRepo()
    {
        //Arrange
        var classRepository = new ClassRepository();
        //Act
        var actual = classRepository.SubclassExists(ClassData1.Name, Subclass1ForClass1.Name);
        //Assert
        Assert.That(actual, Is.False);
    }
    
    [Test]
    [Description("On an existing Class with existing subclasses, " +
                 "SubclassExists with an arbitrary other name is returns false")]
    public void SubclassNotExistsWithOtherExistingSubclasses()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        classRepository.AddSubclass(Subclass1ForClass1);
        classRepository.AddSubclass(Subclass2ForClass1);
        //Act
        var actual = classRepository.SubclassExists(ClassData1.Name, "AnActuallyNotExistantName");
        //Assert
        Assert.That(actual, Is.False);
    }
    
    [Test]
    [Description(
        "When there is a subclass in one class, SubclassExists with that name but another class returns false")]
    public void SubclassExistsWithSameName()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        classRepository.AddClass(ClassData2);
        classRepository.AddSubclass(Subclass1ForClass1);
        //Act
        var actual = classRepository.SubclassExists(ClassData2.Name, Subclass1ForClass1.Name);
        //Assert
        Assert.That(actual, Is.False);
    }
    
    [Test]
    [Description("GetClass will return ClassData equal to the ClassData given in at the beginning")]
    public void GetSubclass()
    {
        //Arrange
        var classRepository = new ClassRepository();
        classRepository.AddClass(ClassData1);
        classRepository.AddSubclass(Subclass1ForClass1);
        //Act
        var actual = classRepository.GetSubclass(ClassData1.Name, Subclass1ForClass1.Name);
        //Assert
        Assert.That(actual, Is.EqualTo(Subclass1ForClass1));
    }
}