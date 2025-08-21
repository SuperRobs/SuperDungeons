using SuperDungeons.Model.Rules.Features;

// ReSharper disable InconsistentNaming

namespace Tests.ModelTests.Features;

public static class FeatureIDFactory
{
    public static FeatureIdentifier Example()
    {
        return new FeatureIdentifier("Example", "Example");
    }

    public static FeatureIdentifier Example2()
    {
        return new FeatureIdentifier("Example2", "Example2");
    }


    public static FeatureIdentifier SameTitleDiffSource_A()
    {
        return new FeatureIdentifier("Title", "SourceA");  
    }

    public static FeatureIdentifier SameTitleDiffSource_B()
    {
        return new FeatureIdentifier("Title", "SourceB");
    }

    public static FeatureIdentifier DiffTitleSameSource_A()
    {
        return new FeatureIdentifier("TitleA", "Source");
    }

    public static FeatureIdentifier DiffTitleSameSource_B()
    {
        return new FeatureIdentifier("TitleB", "Source");
    }
}