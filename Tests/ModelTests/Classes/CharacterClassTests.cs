using static NUnit.Framework.Assert;

namespace Tests.ModelTests.Classes;

[TestFixture]
public class CharacterClassTests
{
    private readonly string _class1 = "Cleric";
    private readonly string _class2 = "Druid";
    
    //Getters
    
    [Test]
    public void IsInitiallyEmpty()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        var actual = classes.GetClassNames();
        That(actual, Is.Empty);
    }

    [Test]
    public void EmptyHasNoLevels()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        var actual = classes.GetCharacterLevel();
        var expected = 0;
        That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void EmptyHasNoPrimaryClass()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        var actual = classes.GetPrimaryClass();
        That(actual, Is.Null);
    }
    
    //We do not enforce PB behaviour for the empty case, it is effectively undefined

    [Test]
    public void SingleClassClassNames()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        List<string> expected = [_class1];
        var actual = classes.GetClassNames();
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void SingleClassPrimaryClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        var expected = _class1;
        var actual = classes.GetPrimaryClass();
        That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void SingleClassCharacterLevel()
    {
        const int expected = 3;
        var classes = CharacterClassFactory.GetSingleClass(_class1, expected);
        var actual = classes.GetCharacterLevel();
        That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void SingleClassClassLevel()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 3);
        var expected = 3;
        var actual = classes.GetClassLevel(_class1);
        That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MultiClassCharacterLevel()
    {
        var classes = CharacterClassFactory.GetDualClass(_class1, 2, _class2, 4);
        var expected = 6;
        var actual = classes.GetCharacterLevel();
        That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MultiClassClassNames()
    {
        var classes = CharacterClassFactory.GetDualClass(_class1, 2, _class2, 4);
        List<string> expected = [_class1, _class2];
        var actual = classes.GetClassNames();
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void MultiClassPrimaryClass()
    {
        var classes = CharacterClassFactory.GetDualClass(_class1, 2, _class2, 4);
        var expected = _class1;
        var actual = classes.GetPrimaryClass();
        That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulticlassPrimaryClassLevel()
    {
        var classes = CharacterClassFactory.GetDualClass(_class1, 2, _class2, 4);
        var expected = 2;
        var actual = classes.GetClassLevel(_class1);
        That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void MulticlassSecondaryClassLevel()
    {
        var classes = CharacterClassFactory.GetDualClass(_class1, 2, _class2, 4);
        var expected = 4;
        var actual = classes.GetClassLevel(_class2);
        That(actual, Is.EqualTo(expected));
    }
    
    //proficiency bonus is just tested at more or less random levels

    [Test]
    public void ProficiencyBonus1()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        const int expected = 2;
        var actual = classes.GetProficiencyBonus();
        That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ProficiencyBonus4()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 4);
        var expected = 2;
        var actual = classes.GetProficiencyBonus();
        That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ProficiencyBonus5()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 5);
        var expected = 3;
        var actual = classes.GetProficiencyBonus();
        That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ProficiencyBonus10()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 10);
        var expected = 4;
        var actual = classes.GetProficiencyBonus();
        That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ProficiencyBonus16()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 16);
        var expected = 5;
        var actual = classes.GetProficiencyBonus();
        That(actual, Is.EqualTo(expected));
    }
    
    //Update Methods, we only test if GetClassNames/GetClassLevel returns the correct results for these
    //It is thus assumed, that the other properties must then also behave correctly
    [Test]
    public void AddClassFromEmpty()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        classes.AddClass(_class1, 1);
        var actual = classes.GetClassNames();
        List<string> expected = [_class1];
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void AddClassFromSingleClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        classes.AddClass(_class2, 1);
        var actual = classes.GetClassNames();
        List<string> expected = [_class1, _class2];
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void AddSameClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        Catch<Exception>(() =>classes.AddClass(_class1, 1));
        var actual = classes.GetClassNames();
        List<string> expected = [_class1];
        CollectionAssert.AreEqual(expected, actual);
    }
    
    [Test]
    public void AddClassLevel0()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        Catch(() =>classes.AddClass(_class1, 0));
        var actual = classes.GetClassNames();
        That(actual, Is.Empty);
    }

    [Test]
    public void AddClassLevel21()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        Catch<Exception>(() =>classes.AddClass(_class1, 21));
        var actual = classes.GetClassNames();
        That(actual, Is.Empty);
    }
    
    [Test]
    public void AddClassLevel20()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        classes.AddClass(_class1, 20);
        var actual = classes.GetClassNames();
        List<string> expected = [_class1];
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void AddClassPushingTotalLevelTo21()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 11);
        Catch<Exception>(() =>classes.AddClass(_class2, 10));
        var actual = classes.GetClassNames();
        List<string> expected = [_class1];
        CollectionAssert.AreEqual(expected, actual);
    }
    
    [Test]
    public void AddClassPushingTotalLevelTo20()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 10);
        classes.AddClass(_class2, 10);
        var actual = classes.GetClassNames();
        List<string> expected = [_class1, _class2];
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void AddClassWithCorrectLevel()
    {
        var classes = CharacterClassFactory.GetUninitialised();
        classes.AddClass(_class1, 5);
        var actual = classes.GetClassLevel(_class1);
        var expected = 5;
        That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ChangeClassLevelOfExistingClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        classes.ChangeClassLevel(_class1, 2);
        var actual = classes.GetClassLevel(_class1);
        var expected = 2;
        That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ChangeClassLevelOfMulticlass()
    {
        var classes = CharacterClassFactory.GetDualClass(_class1, 2, _class2, 4);
        classes.ChangeClassLevel(_class1, 3);
        var actual1 = classes.GetClassLevel(_class1);
        var actual2 = classes.GetClassLevel(_class2);
        var expected1 = 3;
        var expected2 = 4;
        That(expected1, Is.EqualTo(actual1));
        That(expected2, Is.EqualTo(actual2));
    }

    [Test]
    public void ChangeClassLevelTo0()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        Catch<Exception>(() => classes.ChangeClassLevel(_class1, 0));
        var actual = classes.GetClassLevel(_class1);
        var expected = 1;
        That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ChangeClassLevelTo20()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        classes.ChangeClassLevel(_class1, 20);
        var actual = classes.GetClassLevel(_class1);
        var expected = 20;
        That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ChangeClassLevelTo21()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        Catch<Exception>(() => classes.ChangeClassLevel(_class1, 21));
        var actual = classes.GetClassLevel(_class1);
        var expected = 1;
        That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ChangeClassLevelToTotal20()
    {
        var classes = CharacterClassFactory.GetDualClass(_class1, 1, _class2, 2);
        classes.ChangeClassLevel(_class1, 18);
        var actual = classes.GetClassLevel(_class1);
        var expected = 18;
        That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ChangeClassLevelToTotal21()
    {
        var classes = CharacterClassFactory.GetDualClass(_class1, 1, _class2, 10);
        Catch<Exception>(() => classes.ChangeClassLevel(_class1, 11));
        var actual = classes.GetClassLevel(_class1);
        var expected = 1;
        That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ChangeClassLevelOfNonExistingClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        Catch<Exception>(() => classes.ChangeClassLevel(_class2, 2));
    }

    [Test]
    public void ChangePrimaryClassToOtherClass()
    {
        var classes = CharacterClassFactory.GetDualClass(_class1, 1, _class2, 1);
        classes.ChangePrimaryClass(_class2);
        var actual = classes.GetPrimaryClass();
        var expected = _class2;
        That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ChangePrimaryClassToSameClass()
    {
        var classes = CharacterClassFactory.GetDualClass(_class1, 1, _class2, 1);
        classes.ChangePrimaryClass(_class1);
        var actual = classes.GetPrimaryClass();
        var expected = _class1;
        That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ChangePrimaryClassToNonExistingClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        Catch(() => classes.ChangePrimaryClass(_class2));
        var actual = classes.GetPrimaryClass();
        var expected = _class1;
        That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void RemoveExistingClass()
    {
        var classes = CharacterClassFactory.GetDualClass(_class1, 1, _class2, 1);
        classes.RemoveClass(_class2);
        var actual = classes.GetClassNames();
        List<string> expected = [_class1];
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void RemoveNonExistingClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        Catch(() => classes.RemoveClass(_class2));
        var actual = classes.GetClassNames();
        List<string> expected = [_class1];
        That(actual, Is.EqualTo(expected));
    }

    [Test]
    //Even though (for now) an empty classes object can exist, we do not allow the last class to be removed, because a
    //character without classes is an illegal domain state. While we tolerate this state for testing and initialization
    //purposes, we do not want to allow the user to revert to that illegal state once a legal state is established
    public void RemoveLastClass()
    {
        var classes = CharacterClassFactory.GetSingleClass(_class1, 1);
        Catch(() =>classes.RemoveClass(_class1));
        var actual = classes.GetClassNames();
        List<string> expected = [_class1];
        That(actual, Is.EqualTo(expected));
    }
}