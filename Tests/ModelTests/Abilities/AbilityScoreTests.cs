using SuperDungeons.Model.Character.Abilities;
using SuperDungeons.Model.Rules.Features;
using Tests.ModelTests.Features;

namespace Tests.ModelTests.Abilities;

[TestFixture]
public class AbilityScoreTests
{
    private readonly FeatureIdentifier _id = FeatureIDFactory.Example();
    private readonly FeatureIdentifier _id2 = FeatureIDFactory.Example2();
    
    //Getters
    [Test]
    public void GetAbilityScoreWhenZero()
    {
        var zeros = AbilityScoreFactory.AllZero();
        var actual = zeros.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(0));
    }
    
    [Test]
    public void GetAbilityScoreWhenTen()
    {
        var zeros = AbilityScoreFactory.AllTen();
        var actual = zeros.GetAbilityScore(Ability.Dexterity);
        Assert.That(actual, Is.EqualTo(10));
    }
    
    [Test]
    public void GetAbilityScoreWhenTwenty()
    {
        var zeros = AbilityScoreFactory.AllTwenty();
        var actual = zeros.GetAbilityScore(Ability.Constitution);
        Assert.That(actual, Is.EqualTo(20));
    }

    [Test]
    public void GetAbilityModifierNegativeOdd()
    {
        var nines = AbilityScoreFactory.SimpleAbilityScores(9);
        var actual = nines.GetAbilityModifier(Ability.Intelligence);
        Assert.That(actual, Is.EqualTo(-1));
    }

    [Test]
    public void GetAbilityModifierPositiveOdd()
    {
        var thirteens = AbilityScoreFactory.SimpleAbilityScores(13);
        var actual = thirteens.GetAbilityModifier(Ability.Constitution);
        Assert.That(actual, Is.EqualTo(1));
    }

    [Test]
    public void GetAbilityModifierNegativeEven()
    {
        var eights = AbilityScoreFactory.SimpleAbilityScores(8);
        var actual = eights.GetAbilityModifier(Ability.Dexterity);
        Assert.That(actual, Is.EqualTo(-1));
    }

    [Test]
    public void GetAbilityModifierPositiveEven()
    {
        var twelves = AbilityScoreFactory.SimpleAbilityScores(12);
        var actual = twelves.GetAbilityModifier(Ability.Constitution);
        Assert.That(actual, Is.EqualTo(1));
    }

    [Test]
    public void GetAbilityModifierZero()
    {
        var tens = AbilityScoreFactory.AllTen();
        var actual = tens.GetAbilityModifier(Ability.Dexterity);
        Assert.That(actual, Is.EqualTo(0));
    }
    
    //Overrides
    [Test]
    public void HigherOverride()
    {
        var scores = AbilityScoreFactory.AllZero();
        scores.AddOverride(Ability.Strength, _id, 10);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(10));
    }

    [Test]
    public void LowerOverride()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddOverride(Ability.Strength, _id, 9);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(10));
    }
    
    [Test]
    public void EqualOverride()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddOverride(Ability.Strength, _id, 10);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(10));
    }

    [Test]
    public void MultipleOverridesSameAbility()
    {
        var scores = AbilityScoreFactory.AllZero();
        scores.AddOverride(Ability.Strength, _id, 10);
        scores.AddOverride(Ability.Strength, _id2, 20);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(20));
    }
    
    [Test]
    public void MultipleOverridesDifferentAbility()
    {
        var scores = AbilityScoreFactory.AllZero();
        scores.AddOverride(Ability.Strength, _id, 10);
        scores.AddOverride(Ability.Dexterity, _id2, 20);
        var actualStrength =  scores.GetAbilityScore(Ability.Strength);
        var actualDexterity = scores.GetAbilityScore(Ability.Dexterity);
        Assert.Multiple(() =>
        {
            Assert.That(actualStrength, Is.EqualTo(10));
            Assert.That(actualDexterity, Is.EqualTo(20));
        });
    }

    [Test]
    public void RemoveOverride()
    {
        var scores = AbilityScoreFactory.AllZero();
        scores.AddOverride(Ability.Strength, _id, 10);
        scores.RemoveOverride(Ability.Strength, _id);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(0));
    }
    
    [Test]
    public void RemoveInexistentOverride()
    {
        var scores = AbilityScoreFactory.AllZero();
        scores.AddOverride(Ability.Strength, _id, 10);
        scores.RemoveOverride(Ability.Strength, _id2);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(10));
    }
    
    [Test]
    public void RemoveOneOfTwoOverrides()
    {
        var scores = AbilityScoreFactory.AllZero();
        scores.AddOverride(Ability.Strength, _id, 10);
        scores.AddOverride(Ability.Strength, _id2, 20);
        scores.RemoveOverride(Ability.Strength, _id2);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(10));
    }

    [Test]
    public void ChangeOverride()
    {
        var scores = AbilityScoreFactory.AllZero();
        scores.AddOverride(Ability.Strength, _id, 20);
        scores.AddOverride(Ability.Strength, _id, 10);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(10));
    }
    
    //Bonuses 
    [Test]
    public void PositiveBonus()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 2);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(12));
    }
    
    [Test]
    public void NegativeBonus()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, -2);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(8));
    }
    
    [Test]
    public void ZeroBonus()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 0);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(10));
    }

    [Test]
    public void MultiplePositiveBonusesSameAbility()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 1);
        scores.AddBonus(Ability.Strength, _id2, 2);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(13));
    }
    
    [Test]
    public void MultiplePositiveBonusesDifferentAbility()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 1);
        scores.AddBonus(Ability.Dexterity, _id2, 2);
        var actualStrength = scores.GetAbilityScore(Ability.Strength);
        var actualDexterity = scores.GetAbilityScore(Ability.Dexterity);
        Assert.Multiple(() =>
        {
            Assert.That(actualStrength, Is.EqualTo(11));
            Assert.That(actualDexterity, Is.EqualTo(12));
        });
    }

    [Test]
    public void MixedPositiveAndNegativeBonuses()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 1);
        scores.AddBonus(Ability.Strength, _id2, -2);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(9));
    }

    [Test]
    public void NegativeBonusesBelowZero()
    {
        var scores = AbilityScoreFactory.AllZero();
        scores.AddBonus(Ability.Strength, _id, -1);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(0));
    }

    [Test]
    public void MixedNegativeAndPositiveBelowZero()
    {
        var scores = AbilityScoreFactory.AllZero();
        scores.AddBonus(Ability.Strength, _id, -1);
        scores.AddBonus(Ability.Strength, _id2, 2);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(1));
    }

    [Test]
    public void PositiveBonusesAboveTwenty()
    {
        var scores = AbilityScoreFactory.AllTwenty();
        scores.AddBonus(Ability.Strength, _id, 1);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(20));
    }
    
    [Test]
    public void MixedPositiveAndNegativeBonusesAboveTwenty()
    {
        var scores = AbilityScoreFactory.AllTwenty();
        scores.AddBonus(Ability.Strength, _id, 1);
        scores.AddBonus(Ability.Strength, _id2, -2);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(19));
    }
    
    [Test]
    public void RemoveBonus()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 2);
        scores.RemoveBonus(Ability.Strength, _id);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(10));
    }
    
    [Test]
    public void RemoveInexistentBonus()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 2);
        scores.RemoveBonus(Ability.Strength, _id2);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(12));
    }
    
    [Test]
    public void RemoveOneOfTwoBonuses()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 1);
        scores.AddBonus(Ability.Strength, _id2, 2);
        scores.RemoveBonus(Ability.Strength, _id2);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(11));
    }

    [Test]
    public void ChangeBonus()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 2);
        scores.AddBonus(Ability.Strength, _id, 1);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(11));
    }
    
    //Bonuses with caps
    [Test]
    public void BonusWithCap()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 2, 30);
        var actual =  scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(12));
    }

    [Test]
    public void BonusOverCap()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id,2, 10);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(10));
    }

    [Test]
    public void BonusesWithDifferentCapsShouldStack()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 2, 30);
        scores.AddBonus(Ability.Strength, _id2, 2, 12);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(14));
    }

    [Test]
    public void BonusesWithDifferentCapsStillUseCaps()
    {
        var scores =  AbilityScoreFactory.AllTen();
        scores.AddBonus(Ability.Strength, _id, 2, 30);
        scores.AddBonus(Ability.Strength, _id2, 2, 10);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(12));
    }
    //Overrides + Bonuses
    [Test]
    public void HigherOverrideThanBonus()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddOverride(Ability.Strength, _id, 15);
        scores.AddBonus(Ability.Strength, _id2, 2);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(15));
    }

    [Test]
    public void HigherBonusThanOverride()
    {
        var scores = AbilityScoreFactory.AllTen();
        scores.AddOverride(Ability.Strength, _id, 11);
        scores.AddBonus(Ability.Strength, _id2, 2);
        var actual = scores.GetAbilityScore(Ability.Strength);
        Assert.That(actual, Is.EqualTo(12));
    }
    //PropertyChanged triggers
    //Whether PropertyChanged is called when the value doesn't change is undefined
    [Test]
    public void PropertyChangedOnBonusAdded()
    {
        var scores =AbilityScoreFactory.AllTen();
        var propertyChanged = false;
        scores.PropertyChanged += (_, _) => propertyChanged = true;
        scores.AddBonus(Ability.Strength, _id, 2);
        Assert.That(propertyChanged, Is.True);
    }
    
    [Test]
    public void PropertyChangedOnBonusRemoved()
    {
        var scores =AbilityScoreFactory.AllTen();
        var propertyChanged = false;
        scores.AddBonus(Ability.Strength, _id, 2);
        scores.PropertyChanged += (_, _) => propertyChanged = true;
        scores.RemoveBonus(Ability.Strength, _id);
        Assert.That(propertyChanged, Is.True);
    }

    [Test]
    public void PropertyChangedOnOverrideAdded()
    {
        var scores =AbilityScoreFactory.AllTen();
        var propertyChanged = false;
        scores.PropertyChanged += (_, _) => propertyChanged = true;
        scores.AddOverride(Ability.Strength, _id, 20);
        Assert.That(propertyChanged, Is.True);
    }
    
    [Test]
    public void PropertyChangedOnOverrideRemoved()
    {
        var scores =AbilityScoreFactory.AllTen();
        var propertyChanged = false;
        scores.AddOverride(Ability.Strength, _id, 20);
        scores.PropertyChanged += (_, _) => propertyChanged = true;
        scores.RemoveOverride(Ability.Strength, _id);
        Assert.That(propertyChanged, Is.True);
    }
}