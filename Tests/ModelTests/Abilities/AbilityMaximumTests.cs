using SuperDungeons.Model.Abilities;
using SuperDungeons.Model.DataTypes.Enums;

namespace Tests.ModelTests.Abilities;

[TestFixture]
public class AbilityMaximumTests
{
    //All these tests set the base value at 30, this is above the standard maximum, so the results will always
    //be the maximum instead of that actual value
    [Test]
    [Description("Adding a bonus of 1, increases the maximum by one")]
    public void Bonus()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Change, Ability.Strength, "Test", 1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(21));
    }

    [Test]
    [Description("Adding a negative bonus of 1 decreases the maximum by one")]
    public void NegativeBonus()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Change, Ability.Strength, "Test", -1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(19));
    }
    
    [Test]
    [Description("Adding a bonus, then removing it will set AS back to base maximum")]
    public void BonusAddThenRemove()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Change, Ability.Strength, "Test", 1);
        scores.RemoveBonus(BonusTargets.Maximum, BonusTypes.Change, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(20));
    }

    [Test]
    [Description("Removing non-existent bonus does nothing")]
    public void RemoveNonExistentBonus()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.RemoveBonus(BonusTargets.Maximum, BonusTypes.Change, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(20));
    }

    [Test]
    [Description("Adding a bonus, the removing a different one changes the maximum")]
    public void AddBonusRemoveDifferentBonus()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Change, Ability.Strength, "Test", 1);
        scores.RemoveBonus(BonusTargets.Maximum, BonusTypes.Change, Ability.Strength, "Other");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(21));
    }

    [Test]
    [Description("Adding a maximum bonus, then clearing resets maximum (and everything else) back to base value")]
    public void BonusAddThenReset()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Change, Ability.Strength, "Test", 1);
        scores.Reset();
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(20));
    }

    [Test]
    [Description("Overriding the maximum returns the value of the override")]
    public void Override()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Fixed, Ability.Strength, "Test", 21);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(21));
    }

    [Test]
    [Description("Negative override is not used")]
    //the current implementation also writes a message to Debug, however that is not relevant to the functionality,
    //so I will not test it
    public void NegativeOverride()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Fixed, Ability.Strength, "Test", -1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(20));
    }
    
    [Test]
    [Description("Adding an override below the base value will set the maximum to that value")]
    public void OverrideBelowBaseValue()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Fixed, Ability.Strength, "Test", 10);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }

    [Test]
    [Description("Adding a maximum override, then removing it will set maximum back to base value")]
    public void OverrideAddThenRemove()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Fixed, Ability.Strength, "Test", 21);
        scores.RemoveBonus(BonusTargets.Maximum, BonusTypes.Fixed, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(20));
    }

    [Test]
    [Description("Removing non-existent override does nothing")]
    public void RemoveNonExistentOverride()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.RemoveBonus(BonusTargets.Maximum, BonusTypes.Fixed, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(20));
    }

    [Test]
    [Description("Adding an override, the removing a different one changes the maximum to the first override")]
    public void AddOverrideRemoveDifferentOverride()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Fixed, Ability.Strength, "Test", 21);
        scores.RemoveBonus(BonusTargets.Maximum, BonusTypes.Fixed, Ability.Strength, "Other");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(21));
    }

    [Test]
    [Description("Adding an override, then clearing resets maximum back to base value")]
    public void OverrideAddThenReset()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Fixed, Ability.Strength, "Test", 21);
        scores.Reset();
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(20));
    }

    [Test]
    [Description("Adding an override and bonus sets maximum to override + bonus")]
    public void BonusAndOverride()
    {
        //Arrange
        AbilityScores scores = new(30, 30, 30, 30, 30, 30);
        //Act
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Fixed, Ability.Strength, "Test", 21);
        scores.AddBonus(BonusTargets.Maximum, BonusTypes.Change, Ability.Strength, "Test", 1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(22));
    }
}