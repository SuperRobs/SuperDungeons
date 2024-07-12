using SuperDungeons.Model.Abilities;
using SuperDungeons.Model.DataTypes.Enums;

namespace Tests.ModelTests.Abilities;

[TestFixture]
public class AbilityScoreTests
{
    //should these tests become too slow, for the most part one AbilityScores Object (that is cleared between uses
    //would suffice
    
    //these test cases will test all the ability scores at once, this breaks the principle that tests should only ever
    //test one thing, however as all stats should be handled the same, it would just take a lot longer to write separate
    //tests for all abilities
    [Test]
    [Description("When only given base scores return the same values with GetAbilityScore")]
    public void BaseOnly()
    {
        //Arrange/Act
        AbilityScores scores = new(8, 9, 10, 11, 12, 13);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(8));
            Assert.That(scores.GetAbilityScore(Ability.Dexterity), Is.EqualTo(9));
            Assert.That(scores.GetAbilityScore(Ability.Constitution), Is.EqualTo(10));
            Assert.That(scores.GetAbilityScore(Ability.Wisdom), Is.EqualTo(11));
            Assert.That(scores.GetAbilityScore(Ability.Intelligence), Is.EqualTo(12));
            Assert.That(scores.GetAbilityScore(Ability.Charisma), Is.EqualTo(13));
        });
    }

    [Test]
    [Description("Given the same values as the last test, return the correct modifiers")]
    public void Modifiers()
    {
        //Arrange/Act
        AbilityScores scores = new(8, 9, 10, 11, 12, 13);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(scores.GetAbilityModifier(Ability.Strength), Is.EqualTo(-1));
            Assert.That(scores.GetAbilityModifier(Ability.Dexterity), Is.EqualTo(-1));
            Assert.That(scores.GetAbilityModifier(Ability.Constitution), Is.EqualTo(0));
            Assert.That(scores.GetAbilityModifier(Ability.Wisdom), Is.EqualTo(0));
            Assert.That(scores.GetAbilityModifier(Ability.Intelligence), Is.EqualTo(1));
            Assert.That(scores.GetAbilityModifier(Ability.Charisma), Is.EqualTo(1));
        });
    }
    
    [Test]
    [Description("Given some more extreme scores (at the edges of the normal minimums and maximums) return the" +
                 "correct modifiers")]
    public void Extreme()
    {
        //Arrange/Act
        AbilityScores scores = new(0, 2, 3, 18, 19, 20);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(scores.GetAbilityModifier(Ability.Strength), Is.EqualTo(-5));
            Assert.That(scores.GetAbilityModifier(Ability.Dexterity), Is.EqualTo(-4));
            Assert.That(scores.GetAbilityModifier(Ability.Constitution), Is.EqualTo(-4));
            Assert.That(scores.GetAbilityModifier(Ability.Wisdom), Is.EqualTo(4));
            Assert.That(scores.GetAbilityModifier(Ability.Intelligence), Is.EqualTo(4));
            Assert.That(scores.GetAbilityModifier(Ability.Charisma), Is.EqualTo(5));
        });
    }

    //in the following all tests will target only the strength score, it is assumed that all ASs work the same
    [Test]
    [Description("Adding a bonus of 1, increases the AS by one")]
    public void Bonus()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Change, Ability.Strength, "Test", 1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(11));
    }

    [Test]
    [Description("Adding a negative bonus of 1 decreases the AS by one")]
    public void NegativeBonus()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Change, Ability.Strength, "Test", -1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(9));
    }
    
    [Test]
    [Description("Adding a score bonus, then removing it will set AS back to base value")]
    public void BonusAddThenRemove()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Change, Ability.Strength, "Test", 1);
        scores.RemoveBonus(BonusTargets.Score, BonusTypes.Change, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }

    [Test]
    [Description("Removing non-existent bonus does nothing")]
    public void RemoveNonExistentBonus()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.RemoveBonus(BonusTargets.Score, BonusTypes.Change, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }

    [Test]
    [Description("Adding a bonus, the removing a different one changes the AS")]
    public void AddBonusRemoveDifferentBonus()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Change, Ability.Strength, "Test", 1);
        scores.RemoveBonus(BonusTargets.Score, BonusTypes.Change, Ability.Strength, "Other");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(11));
    }

    [Test]
    [Description("Adding a score bonus, then clearing resets AS back to base value")]
    public void BonusAddThenReset()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Change, Ability.Strength, "Test", 1);
        scores.Reset();
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }

    [Test]
    [Description("Overriding the score returns the value of the override")]
    public void Override()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Fixed, Ability.Strength, "Test", 11);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(11));
    }

    [Test]
    [Description("Negative override is not used")]
    //the current implementation also writes a message to Debug, however that is not relevant to the functionality,
    //so I will not test it
    public void NegativeOverride()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Fixed, Ability.Strength, "Test", -1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }
    
    [Test]
    [Description("Adding an override below the base value will set the AS to that value")]
    public void OverrideBelowBaseValue()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Fixed, Ability.Strength, "Test", 9);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(9));
    }

    [Test]
    [Description("Adding an override, then removing it will set AS back to base value")]
    public void OverrideAddThenRemove()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Fixed, Ability.Strength, "Test", 11);
        scores.RemoveBonus(BonusTargets.Score, BonusTypes.Fixed, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }

    [Test]
    [Description("Removing non-existent override does nothing")]
    public void RemoveNonExistentOverride()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.RemoveBonus(BonusTargets.Score, BonusTypes.Fixed, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }

    [Test]
    [Description("Adding an override, the removing a different one changes the AS")]
    public void AddOverrideRemoveDifferentOverride()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Fixed, Ability.Strength, "Test", 11);
        scores.RemoveBonus(BonusTargets.Score, BonusTypes.Fixed, Ability.Strength, "Other");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(11));
    }

    [Test]
    [Description("Adding an override, then clearing resets AS back to base value")]
    public void OverrideAddThenReset()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Fixed, Ability.Strength, "Test", 1);
        scores.Reset();
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }

    [Test]
    [Description("Adding a score override and bonus sets AS to override + bonus")]
    public void BonusAndOverride()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Fixed, Ability.Strength, "Test", 11);
        scores.AddBonus(BonusTargets.Score, BonusTypes.Change, Ability.Strength, "Test", 1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(12));
    }

    [Test]
    [Description("Setting an Override higher than the standard maximum sets the AS to the standard maximum")]
    public void OverrideAboveMaximum()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Score, BonusTypes.Fixed, Ability.Strength, "Test", 21);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(20));
    }
    
    //Reset
    
    //Test cases necessary
    //for each target
        //for eachType
        //it suffices to check strength for all
            //check if setting, removing, and overriding work
            //check positive and negative values
            //check if clearing works
            //check if mixed bonuses and overrides work
    
    
}