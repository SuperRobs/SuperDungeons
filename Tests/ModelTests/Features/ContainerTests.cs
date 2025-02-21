using System.ComponentModel;
using Moq;
using SuperDungeons.Model.Classes;
using SuperDungeons.Model.Features;
using SuperDungeons.Model.Features.Types.Containers;

namespace Tests.ModelTests.Features;

[TestFixture]
public class ContainerTests
{
    private const string ClassIdentifier = "SomeClass";
    private static readonly FeatureIdentifier GenericIdentifier = new FeatureIdentifier("Title", "Source");
    private const string GenericDescription = "Description";
    
    //CHARACTER level restricted
    [Test]
    [NUnit.Framework.Description("When CharacterLevel is higher than necessary, Applying the " +
                                 "CharacterLevelRestrictedFeature will apply the contained feature")]
    public void CharacterLevelRestrictedFeatureFulfilledPrerequisite()
    {
        //Arrange
        var classManager = new Mock<IClassManager>();
        classManager.Setup(m => m.GetCharacterLevel()).Returns(10);
        var subfeature = new Mock<IFeature>();
        var feature = new CharacterLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, 5, subfeature.Object);
        //Act
        feature.Apply();
        //Assert
        subfeature.Verify(m => m.Apply(), Times.Once);
    }
    
    [Test]
    [NUnit.Framework.Description("When CharacterLevel is high enough, Applying the " +
                                 "CharacterLevelRestrictedFeature will apply the contained feature")]
    public void CharacterLevelRestrictedFeatureFulfilledPrerequisiteExactly()
    {
        //Arrange
        var classManager = new Mock<IClassManager>();
        classManager.Setup(m => m.GetCharacterLevel()).Returns(5);
        var subfeature = new Mock<IFeature>();
        var feature = new CharacterLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, 5, subfeature.Object);
        //Act
        feature.Apply();
        //Assert
        subfeature.Verify(m => m.Apply(), Times.Once);
    }
    
    [Test]
    [NUnit.Framework.Description("When CharacterLevel is below the threshold, Applying the " +
                                 "CharacterLevelRestrictedFeature will apply the contained feature")]
    public void CharacterLevelRestrictedFeaturePrerequisiteNotFulfilled()
    {
        //Arrange
        var classManager = new Mock<IClassManager>();
        classManager.Setup(m => m.GetCharacterLevel()).Returns(1);
        var subfeature = new Mock<IFeature>();
        var feature = new CharacterLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, 5, subfeature.Object);
        //Act
        feature.Apply();
        //Assert
        subfeature.Verify(m => m.Apply(), Times.Never);
    }
    
    [Test]
    [NUnit.Framework.Description("When CharacterLevel is one below the threshold, Applying the " +
                                 "CharacterLevelRestrictedFeature will apply the contained feature")]
    public void CharacterLevelRestrictedFeaturePrerequisiteNotFulfilledByOne()
    {
        //Arrange
        var classManager = new Mock<IClassManager>();
        classManager.Setup(m => m.GetCharacterLevel()).Returns(4);
        var subfeature = new Mock<IFeature>();
        var feature = new CharacterLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, 5, subfeature.Object);
        //Act
        feature.Apply();
        //Assert
        subfeature.Verify(m => m.Apply(), Times.Never);
    }
    
    [Test]
    [NUnit.Framework.Description("When Remove is called on a CharLRF is called and the level " +
                                 "is high enough, it is removed")]
    public void CharacterLevelRestrictedFeatureRemovePrerequisiteFulfilled()
    {
        //Arrange
        var classManager = new Mock<IClassManager>();
        classManager.Setup(m => m.GetCharacterLevel()).Returns(10);
        var subfeature = new Mock<IFeature>();
        var feature = new CharacterLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, 5, subfeature.Object);
        //Act
        feature.Remove();
        //Assert
        subfeature.Verify(m => m.Remove(), Times.Once);
    }

    [Test]
    [NUnit.Framework.Description("When a feature is active on a CharLRF and the level reduces, " +
                                 "but not below the threshold, it is not removed")]
    public void CharacterLevelRestrictedFeatureReduceLevelAboveThreshold()
    {
        //Arrange
        //since mocking would be difficult here I'll use a real object
        var classManager = new Mock<IClassManager>();
        classManager.SetupSequence(m => m.GetCharacterLevel()).Returns(10).Returns(9);
        var subfeature = new Mock<IFeature>();
        var feature = new CharacterLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, 5, subfeature.Object);
        //Act
        classManager.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Level"));
        //Assert
        subfeature.Verify(m => m.Remove(), Times.Never);
    }
    
    [Test]
    [NUnit.Framework.Description("When a feature is active on a CharLRF and the level reduces" +
                                 " below the threshold it is removed")]
    public void CharacterLevelRestrictedFeatureReduceLevelBelowThreshold()
    {
        //Arrange
        //since mocking would be difficult here I'll use a real object
        var classManager = new Mock<IClassManager>();
        classManager.SetupSequence(m => m.GetCharacterLevel()).Returns(10).Returns(1);
        var subfeature = new Mock<IFeature>();
        var feature = new CharacterLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, 5, subfeature.Object);
        //Act
        //this triggers the first GetCharacterLevel,
        //after this the feature should be enabled (as tested by another test)
        feature.Apply();
        //this triggers another check and should disable the feature
        classManager.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Level"));
        //Assert
        subfeature.Verify(m => m.Remove(), Times.Once);
    }
    
    //what exactly happens when the level is not high enough is not currently part of the tests
    
    //CLASS level restricted
    [Test]
    [NUnit.Framework.Description("When CharacterLevel is higher than necessary, Applying the " +
                                 "ClassLevelRestrictedFeature will apply the contained feature")]
    public void ClassLevelRestrictedFeatureFulfilledPrerequisite()
    {
        //Arrange
        var classManager = new Mock<IClassManager>();
        classManager.Setup(m => m.GetCharacterClassLevel(ClassIdentifier)).Returns(10);
        var subfeature = new Mock<IFeature>();
        var feature = new ClassLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, ClassIdentifier, 5, subfeature.Object);
        //Act
        feature.Apply();
        //Assert
        subfeature.Verify(m => m.Apply());
        subfeature.Verify(m => m.Remove(), Times.Never);
    }
    
    [Test]
    [NUnit.Framework.Description("When ClassLevel is high enough, Applying the ClassLevelRestrictedFeature will " +
                                 "apply the contained feature")]
    public void ClassLevelRestrictedFeatureFulfilledPrerequisiteExactly()
    {
        //Arrange
        var classManager = new Mock<IClassManager>();
        classManager.Setup(m => m.GetCharacterClassLevel(ClassIdentifier)).Returns(5);
        var subfeature = new Mock<IFeature>();
        var feature = new ClassLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, ClassIdentifier, 5, subfeature.Object);
        //Act
        feature.Apply();
        //Assert
        subfeature.Verify(m => m.Apply(), Times.Once);
    }
    
    [Test]
    [NUnit.Framework.Description("When ClassLevel is below the threshold, Applying the ClassLevelRestrictedFeature " +
                                 "will apply the contained feature")]
    public void ClassLevelRestrictedFeaturePrerequisiteNotFulfilled()
    {
        //Arrange
        var classManager = new Mock<IClassManager>();
        classManager.Setup(m => m.GetCharacterClassLevel(ClassIdentifier)).Returns(1);
        var subfeature = new Mock<IFeature>();
        var feature = new ClassLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, ClassIdentifier, 5, subfeature.Object);
        //Act
        feature.Apply();
        //Assert
        subfeature.Verify(m => m.Apply(), Times.Never);
    }
    
    [Test]
    [NUnit.Framework.Description("When ClassLevel is one below the threshold, Applying the " +
                                 "ClassLevelRestrictedFeature will apply the contained feature")]
    public void ClassLevelRestrictedFeaturePrerequisiteNotFulfilledByOne()
    {
        //Arrange
        var classManager = new Mock<IClassManager>();
        classManager.Setup(m => m.GetCharacterClassLevel(ClassIdentifier)).Returns(4);
        var subfeature = new Mock<IFeature>();
        var feature = new ClassLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, ClassIdentifier, 5, subfeature.Object);
        //Act
        feature.Apply();
        //Assert
        subfeature.Verify(m => m.Apply(), Times.Never);
    }
    
    [Test]
    [NUnit.Framework.Description("When a feature is active on a CharLRF and the level reduces, " +
                                 "but not below the threshold, it is not removed")]
    public void ClassLevelRestrictedFeatureReduceLevelAboveThreshold()
    {
        //Arrange
        //since mocking would be difficult here I'll use a real object
        var classManager = new Mock<IClassManager>();
        classManager.SetupSequence(m => m.GetCharacterClassLevel("Class")).Returns(10).Returns(9);
        var subfeature = new Mock<IFeature>();
        var feature = new ClassLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, "Class", 5, subfeature.Object);
        //Act
        classManager.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Level"));
        //Assert
        subfeature.Verify(m => m.Remove(), Times.Never);
    }
    
    [Test]
    [NUnit.Framework.Description("When a feature is active on a CharLRF and the level reduces" +
                                 " below the threshold it is removed")]
    public void ClassLevelRestrictedFeatureReduceLevelBelowThreshold()
    {
        //Arrange
        //since mocking would be difficult here I'll use a real object
        var classManager = new Mock<IClassManager>();
        classManager.SetupSequence(m => m.GetCharacterClassLevel("Class")).Returns(10).Returns(1);
        var subfeature = new Mock<IFeature>();
        var feature = new ClassLevelRestrictedFeature
            (GenericIdentifier, GenericDescription, classManager.Object, "Class", 5, subfeature.Object);
        //Act
        //this triggers the first GetCharacterLevel,
        //after this the feature should be enabled (as tested by another test)
        feature.Apply();
        //this triggers another check and should disable the feature
        classManager.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Level"));
        //Assert
        subfeature.Verify(m => m.Remove(), Times.Once);
    }

    [Test]
    [NUnit.Framework.Description("When a multiFeature is applied, all children are also applied")]
    public void MultiFeatureAllChildrenApplied()
    {
        //Arrange
        var feature1 = new Mock<IFeature>();
        var feature2 = new Mock<IFeature>();
        var feature3 = new Mock<IFeature>();
        List<IFeature> features = [feature1.Object, feature2.Object, feature3.Object];
        var feature = new MultiFeature(GenericIdentifier, GenericDescription, features);
        //Act
        feature.Apply();
        //Assert
        feature1.Verify(m => m.Apply(), Times.Once);
        feature2.Verify(m => m.Apply(), Times.Once);
        feature3.Verify(m => m.Apply(), Times.Once);
    }
    
    [Test]
    [NUnit.Framework.Description("When a multiFeature is removed, all children are also applied")]
    public void MultiFeatureAllChildrenRemoved()
    {
        //Arrange
        var feature1 = new Mock<IFeature>();
        var feature2 = new Mock<IFeature>();
        var feature3 = new Mock<IFeature>();
        List<IFeature> features = [feature1.Object, feature2.Object, feature3.Object];
        var feature = new MultiFeature(GenericIdentifier, GenericDescription, features);
        //Act
        feature.Remove();
        //Assert
        feature1.Verify(m => m.Remove(), Times.Once);
        feature2.Verify(m => m.Remove(), Times.Once);
        feature3.Verify(m => m.Remove(), Times.Once);
    }
}

//ToDo ClassLevel Remove tests
//ToDo ChoiceFeature tests