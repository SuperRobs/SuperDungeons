namespace Tests.ModelTests.Classes;

[TestFixture]
public class CharacterClassTests
{
    private readonly string class1 = "Cleric";
    private readonly string class2 = "Druid";
    
    //Getters
    
    [Test]
    public void IsInitiallyEmpty()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        var actual = classes.GetClassNames();
        Assert.IsEmpty(actual);
    }

    [Test]
    public void EmptyHasNoLevels()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        var actual = classes.GetCharacterLevel();
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void EmptyHasNoPrimaryClass()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        var actual = classes.GetPrimaryClass();
        Assert.Null(actual);
    }
    
    //We do not enforce PB behaviour for the empty case, it is effectively undefined

    [Test]
    public void SingleClassClassNames()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        List<string> expected = [class1];
        var actual = classes.GetClassNames();
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void SingleClassPrimaryClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        var expected = class1;
        var actual = classes.GetPrimaryClass();
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void SingleClassCharacterLevel()
    {
        const int expected = 3;
        var classes = CharacterClassFactory.GetSingleClass(class1, expected);
        var actual = classes.GetCharacterLevel();
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void SingleClassClassLevel()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 3);
        var expected = 3;
        var actual = classes.GetClassLevel(class1);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void MultiClassCharacterLevel()
    {
        var classes = CharacterClassFactory.GetDualClass(class1, 2, class2, 4);
        var expected = 6;
        var actual = classes.GetCharacterLevel();
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void MultiClassClassNames()
    {
        var classes = CharacterClassFactory.GetDualClass(class1, 2, class2, 4);
        List<string> expected = [class1, class2];
        var actual = classes.GetClassNames();
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void MultiClassPrimaryClass()
    {
        var classes = CharacterClassFactory.GetDualClass(class1, 2, class2, 4);
        var expected = class1;
        var actual = classes.GetPrimaryClass();
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void MulticlassPrimaryClassLevel()
    {
        var classes = CharacterClassFactory.GetDualClass(class1, 2, class2, 4);
        var expected = 2;
        var actual = classes.GetClassLevel(class1);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void MulticlassSecondaryClassLevel()
    {
        var classes = CharacterClassFactory.GetDualClass(class1, 2, class2, 4);
        var expected = 4;
        var actual = classes.GetClassLevel(class2);
        Assert.AreEqual(expected, actual);
    }
    
    //proficiency bonus is just tested at more or less random levels

    [Test]
    public void ProficiencyBonus1()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        var expected = 2;
        var actual = classes.GetProficiencyBonus();
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void ProficiencyBonus4()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 4);
        var expected = 2;
        var actual = classes.GetProficiencyBonus();
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void ProficiencyBonus5()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 5);
        var expected = 3;
        var actual = classes.GetProficiencyBonus();
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void ProficiencyBonus10()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 10);
        var expected = 4;
        var actual = classes.GetProficiencyBonus();
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void ProficiencyBonus16()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 16);
        var expected = 5;
        var actual = classes.GetProficiencyBonus();
        Assert.AreEqual(expected, actual);
    }
    
    //Update Methods, we only test if GetClassNames/GetClassLevel returns the correct results for these
    //It is thus assumed, that the other properties must then also behave correctly
    [Test]
    public void AddClassFromEmpty()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        classes.AddClass(class1, 1);
        var actual = classes.GetClassNames();
        List<string> expected = [class1];
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void AddClassFromSingleClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        classes.AddClass(class2, 1);
        var actual = classes.GetClassNames();
        List<string> expected = [class1, class2];
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void AddSameClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        Assert.Catch<Exception>(() =>classes.AddClass(class1, 1));
        var actual = classes.GetClassNames();
        List<string> expected = [class1];
        CollectionAssert.AreEqual(expected, actual);
    }
    
    [Test]
    public void AddClassLevel0()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        Assert.Catch(() =>classes.AddClass(class1, 0));
        var actual = classes.GetClassNames();
        Assert.IsEmpty(actual);
    }

    [Test]
    public void AddClassLevel21()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        Assert.Catch<Exception>(() =>classes.AddClass(class1, 21));
        var actual = classes.GetClassNames();
        Assert.IsEmpty(actual);
    }
    
    [Test]
    public void AddClassLevel20()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        classes.AddClass(class1, 20);
        var actual = classes.GetClassNames();
        List<string> expected = [class1];
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void AddClassPushingTotalLevelTo21()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 11);
        Assert.Catch<Exception>(() =>classes.AddClass(class2, 10));
        var actual = classes.GetClassNames();
        List<string> expected = [class1];
        CollectionAssert.AreEqual(expected, actual);
    }
    
    [Test]
    public void AddClassPushingTotalLevelTo20()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 10);
        classes.AddClass(class2, 10);
        var actual = classes.GetClassNames();
        List<string> expected = [class1, class2];
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void AddClassWithCorrectLevel()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        classes.AddClass(class1, 5);
        var actual = classes.GetClassLevel(class1);
        var expected = 5;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ChangeClassLevelOfExistingClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        classes.ChangeClassLevel(class1, 2);
        var actual = classes.GetClassLevel(class1);
        var expected = 2;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ChangeClassLevelOfMulticlass()
    {
        var classes = CharacterClassFactory.GetDualClass(class1, 2, class2, 4);
        classes.ChangeClassLevel(class1, 3);
        var actual1 = classes.GetClassLevel(class1);
        var actual2 = classes.GetClassLevel(class2);
        var expected1 = 3;
        var expected2 = 4;
        Assert.AreEqual(actual1, expected1);
        Assert.AreEqual(actual2, expected2);
    }

    [Test]
    public void ChangeClassLevelTo0()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        Assert.Catch<Exception>(() => classes.ChangeClassLevel(class1, 0));
        var actual = classes.GetClassLevel(class1);
        var expected = 1;
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void ChangeClassLevelTo20()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        classes.ChangeClassLevel(class1, 20);
        var actual = classes.GetClassLevel(class1);
        var expected = 20;
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void ChangeClassLevelTo21()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        Assert.Catch<Exception>(() => classes.ChangeClassLevel(class1, 21));
        var actual = classes.GetClassLevel(class1);
        var expected = 1;
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void ChangeClassLevelToTotal20()
    {
        var classes = CharacterClassFactory.GetDualClass(class1, 1, class2, 2);
        classes.ChangeClassLevel(class1, 18);
        var actual = classes.GetClassLevel(class1);
        var expected = 18;
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void ChangeClassLevelToTotal21()
    {
        var classes = CharacterClassFactory.GetDualClass(class1, 1, class2, 10);
        Assert.Catch<Exception>(() => classes.ChangeClassLevel(class1, 11));
        var actual = classes.GetClassLevel(class1);
        var expected = 1;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ChangeClassLevelOfNonExistingClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        Assert.Catch<Exception>(() => classes.ChangeClassLevel(class2, 2));
    }

    [Test]
    public void ChangePrimaryClassToOtherClass()
    {
        var classes = CharacterClassFactory.GetDualClass(class1, 1, class2, 1);
        classes.ChangePrimaryClass(class2);
        var actual = classes.GetPrimaryClass();
        var expected = class2;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ChangePrimaryClassToSameClass()
    {
        var classes = CharacterClassFactory.GetDualClass(class1, 1, class2, 1);
        classes.ChangePrimaryClass(class1);
        var actual = classes.GetPrimaryClass();
        var expected = class1;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ChangePrimaryClassToNonExistingClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        Assert.Catch(() => classes.ChangePrimaryClass(class2));
        var actual = classes.GetPrimaryClass();
        var expected = class1;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void RemoveExistingClass()
    {
        var classes = CharacterClassFactory.GetDualClass(class1, 1, class2, 1);
        classes.RemoveClass(class2);
        var actual = classes.GetClassNames();
        List<string> expected = [class1];
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void RemoveNonExistingClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        Assert.Catch(() => classes.RemoveClass(class2));
        var actual = classes.GetClassNames();
        List<string> expected = [class1];
        Assert.AreEqual(expected, actual);
    }

    [Test]
    //Even though (for now) an empty classes object can exist, we do not allow the last class to be removed, because a
    //character without classes is an illegal domain state. While we tolerate this state for testing and initialization
    //purposes, we do not want to allow the user to revert to that illegal state once a legal state is established
    public void RemoveLastClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(class1, 1);
        Assert.Catch(() =>classes.RemoveClass(class1));
        var actual = classes.GetClassNames();
        List<string> expected = [class1];
        Assert.AreEqual(expected, actual);
    }
}