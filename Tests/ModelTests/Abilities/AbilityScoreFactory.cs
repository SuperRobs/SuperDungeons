using SuperDungeons.Model.Abilities;

namespace Tests.ModelTests.Abilities;

public static class AbilityScoreFactory
{
    public static IAbilityScores SimpleAbilityScores(uint values)
    {
        return new AbilityScores(values, values, values, values, values, values);
    }

    public static IAbilityScores AllZero() {
        return SimpleAbilityScores(0);
    }
    public static IAbilityScores AllTen() {
        return SimpleAbilityScores(10);
    }
    public static IAbilityScores AllTwenty(){
        return SimpleAbilityScores(20);
    }
}