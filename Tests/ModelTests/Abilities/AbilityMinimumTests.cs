using SuperDungeons.Model.Abilities;
using SuperDungeons.Model.DataTypes.Enums;

namespace Tests.ModelTests.Abilities;

[TestFixture]
public class AbilityMinimumTests
{
    //All these tests set the base value at 0, this is below the standard minimum, so the results will always
    //be the minimum instead of that actual value
    [Test]
    [Description("Adding a bonus of 1, increases the minimum by one")]
    public void Bonus()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Change, Ability.Strength, "Test", 1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(2));
    }

    [Test]
    [Description("Adding a negative bonus of 1 decreases the minimum by one")]
    public void NegativeBonus()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);        
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Change, Ability.Strength, "Test", -1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(0));
    }
    
    [Test]
    [Description("Adding a bonus, then removing it will set AS back to base minimum")]
    public void BonusAddThenRemove()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);        
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Change, Ability.Strength, "Test", 1);
        scores.RemoveBonus(BonusTargets.Minimum, BonusTypes.Change, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(1));
    }

    [Test]
    [Description("Removing non-existent bonus does nothing")]
    public void RemoveNonExistentBonus()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);        
        //Act
        scores.RemoveBonus(BonusTargets.Minimum, BonusTypes.Change, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(1));
    }

    [Test]
    [Description("Adding a bonus, the removing a different one changes the minimum")]
    public void AddBonusRemoveDifferentBonus()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);        
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Change, Ability.Strength, "Test", 1);
        scores.RemoveBonus(BonusTargets.Minimum, BonusTypes.Change, Ability.Strength, "Other");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(2));
    }

    [Test]
    [Description("Adding a minimum bonus, then clearing resets minimum (and everything else) back to base value")]
    public void BonusAddThenReset()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);        
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Change, Ability.Strength, "Test", 1);
        scores.Reset();
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(1));
    }

    [Test]
    [Description("Overriding the minimum returns the value of the override")]
    public void Override()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);        
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Fixed, Ability.Strength, "Test", 10);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }

    [Test]
    [Description("Negative override is not used")]
    //the current implementation also writes a message to Debug, however that is not relevant to the functionality,
    //so I will not test it
    public void NegativeOverride()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);       
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Fixed, Ability.Strength, "Test", -1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(1));
    }
    
    [Test]
    [Description("Adding an override below the base value will set the minimum to that value")]
    public void OverrideBelowBaseValue()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);      
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Fixed, Ability.Strength, "Test", 10);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }

    [Test]
    [Description("Adding a minimum override, then removing it will set minimum back to base value")]
    public void OverrideAddThenRemove()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);    
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Fixed, Ability.Strength, "Test", 10);
        scores.RemoveBonus(BonusTargets.Minimum, BonusTypes.Fixed, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(1));
    }

    [Test]
    [Description("Removing non-existent override does nothing")]
    public void RemoveNonExistentOverride()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);    
        //Act
        scores.RemoveBonus(BonusTargets.Minimum, BonusTypes.Fixed, Ability.Strength, "Test");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(1));
    }

    [Test]
    [Description("Adding an override, the removing a different one changes the minimum to the first override")]
    public void AddOverrideRemoveDifferentOverride()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);     
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Fixed, Ability.Strength, "Test", 10);
        scores.RemoveBonus(BonusTargets.Minimum, BonusTypes.Fixed, Ability.Strength, "Other");
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }

    [Test]
    [Description("Adding an override, then clearing resets minimum back to base value")]
    public void OverrideAddThenReset()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);     
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Fixed, Ability.Strength, "Test", 10);
        scores.Reset();
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(1));
    }

    [Test]
    [Description("Adding an override and bonus sets minimum to override + bonus")]
    public void BonusAndOverride()
    {
        //Arrange
        AbilityScores scores = new(0, 0, 0, 0, 0, 0);     
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Fixed, Ability.Strength, "Test", 10);
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Change, Ability.Strength, "Test", 1);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(11));
    }

    [Test]
    [Description("Setting the AS above the minimum returns the actual value")]
    // ReSharper disable once InconsistentNaming AS is short for AbilityScore, so both are capitalized
    public void ASAboveMinimum()
    {
        //Arrange
        AbilityScores scores = new(10, 10, 10, 10, 10, 10);
        //Act
        scores.AddBonus(BonusTargets.Minimum, BonusTypes.Fixed, Ability.Strength, "Test", 3);
        //Assert
        Assert.That(scores.GetAbilityScore(Ability.Strength), Is.EqualTo(10));
    }
}