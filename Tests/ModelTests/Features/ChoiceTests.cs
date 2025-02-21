using System.Collections.Immutable;
using SuperDungeons.Model.Features;

namespace Tests.ModelTests.Features;

//These tests test choices completely isolated from features
[TestFixture]
public class ChoiceTests
{
    private static readonly FeatureIdentifier ParentFeature1 = new("Parent1", "Test");
    private static readonly FeatureIdentifier ParentFeature2 = new("Parent2", "Test");
    private static readonly FeatureIdentifier Choice1 = new("Choice1", "Test");
    private static readonly FeatureIdentifier Choice2 = new("Choice2", "Test");
    private static readonly FeatureIdentifier Choice3 = new("Choice3", "Test");

    [Test]
    [Description("If there are no Choices, GetNChosenFeatures will return an empty list")]
    public void EmptyChoiceGetNChosenFeatures()
    {
        //Arrange
        var choiceManager = new ChoiceManager();
        //Act
        var actual = choiceManager.GetNChosenFeatures(ParentFeature1, 1);
        //Assert
        Assert.That(actual, Is.Empty);
    }
    
    [Test]
    [Description("After adding a choice, GetNChosenFeatures with the appropriate parent will return the added element")]
    public void AddChoice()
    {
        //Arrange
        var choiceManager = new ChoiceManager();
        //Act
        //this tests both the add method and the getNChosenFeatures method, this is unclean, but I don't see a way to
        //do it differently without going into Reflection territory which I just find too complex for such simple tests
        choiceManager.AddChoices(new FeatureChoice(ParentFeature1, [Choice1]));
        var actual = choiceManager.GetNChosenFeatures(ParentFeature1, 1);
        //Assert
        Assert.That(actual, Contains.Item(Choice1));
    }
    
    [Test]
    [Description("After adding multiple choices, GetNChosenFeatures with the appropriate parent and amount will " +
                 "return all the added elements")]
    public void AddMultipleChoice()
    {
        //Arrange
        var choiceManager = new ChoiceManager();
        ImmutableList<FeatureIdentifier> choices = [Choice1, Choice2, Choice3];
        //Act
        //this tests both the add method and the getNChosenFeatures method, this is unclean, but I don't see a way to
        //do it better
        choiceManager.AddChoices(new FeatureChoice(ParentFeature1, choices));
        var actual = choiceManager.GetNChosenFeatures(ParentFeature1, 3);
        //Assert
        Assert.That(actual.All(c => choices.Contains(c)), Is.True);
    }

    [Test]
    [Description("After adding a choice, GetNChosenFeatures with another parent will return an empty list")]
    public void AddOtherChoice()
    {
        //Arrange
        var choiceManager = new ChoiceManager();
        //Act
        //this tests both the add method and the getNChosenFeatures method, this is unclean, but I don't see a way to
        //do it differently without going into Reflection territory which I just find too complex for such simple tests
        choiceManager.AddChoices(new FeatureChoice(ParentFeature1, [Choice1]));
        var actual = choiceManager.GetNChosenFeatures(ParentFeature2, 1);
        //Assert
        Assert.That(actual, Does.Not.Contain(Choice1));
    }

    [Test]
    [Description("After adding a choice and removing it, GetNChosenFeatures with the same parent will return the " +
                 "empty list")]
    public void AddThenRemove()
    {
        //Arrange
        var choiceManager = new ChoiceManager();
        //Act
        choiceManager.AddChoices(new FeatureChoice(ParentFeature1, [Choice1]));
        choiceManager.RemoveChoices(new FeatureChoice(ParentFeature1, [Choice1]));
        var actual = choiceManager.GetNChosenFeatures(ParentFeature1, 1);
        //Arrange
        Assert.That(actual, Is.Empty);
    }
    
    [Test]
    [Description("If there are no Choices, HasChoice returns false")]
    public void EmptyHasChoice()
    {
        //Arrange
        var choiceManager = new ChoiceManager();
        //Act
        var actual = choiceManager.HasChoices(new FeatureChoice(ParentFeature1, [Choice1]));
        //Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    [Description("If there is no choice with the correct parent, HasChoice returns false")]
    public void NoCorrectParentHasChoice()
    {
        //Arrange
        var choiceManager = new ChoiceManager();
        choiceManager.AddChoices(new FeatureChoice(ParentFeature2, [Choice2]));
        //Act
        var actual = choiceManager.HasChoices(new FeatureChoice(ParentFeature1, [Choice1]));
        //Assert
        Assert.That(actual, Is.False);
    }
    
    [Test]
    [Description("If there is no choice with the correct parent, but one with the correct choice, " +
                 "HasChoice returns false")]
    public void NoCorrectParentButCorrectChoiceHasChoice()
    {
        //Arrange
        var choiceManager = new ChoiceManager();
        choiceManager.AddChoices(new FeatureChoice(ParentFeature2, [Choice1]));
        //Act
        var actual = choiceManager.HasChoices(new FeatureChoice(ParentFeature1, [Choice1]));
        //Assert
        Assert.That(actual, Is.False);
    }
    
    [Test]
    [Description("If there is a choice with the correct parent, but not the correct choice name, HasChoice " +
                 "returns false")]
    public void NoCorrectChoiceHasChoice()
    {
        //Arrange
        var choiceManager = new ChoiceManager();
        choiceManager.AddChoices(new FeatureChoice(ParentFeature1, [Choice2]));
        //Act
        var actual = choiceManager.HasChoices(new FeatureChoice(ParentFeature1, [Choice1]));
        //Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    [Description("If there is an appropriate choice, HasChoice returns true")]
    public void CorrectChoiceHasChoice()
    {
        //Arrange
        var choiceManager = new ChoiceManager();
        choiceManager.AddChoices(new FeatureChoice(ParentFeature1, [Choice1]));
        //Act
        var actual = choiceManager.HasChoices(new FeatureChoice(ParentFeature1, [Choice1]));
        //Assert
        Assert.That(actual, Is.True);
    }
    
    [Test]
    [Description("If there are multiple appropriate choices, HasChoice returns true")]
    public void MultipleChoicesHasChoice()
    {
        //Arrange
        var choiceManager = new ChoiceManager();
        choiceManager.AddChoices(new FeatureChoice(ParentFeature1, [Choice1, Choice2, Choice3]));
        //Act
        var actual = choiceManager.HasChoices(new FeatureChoice(ParentFeature1, [Choice1]));
        //Assert
        Assert.That(actual, Is.True);
    }
}