namespace SuperDungeons.Model.Abilities;

public class AbilityScoresBuilder
{
    public static IAbilityScores GetAbilityScores(uint strength, uint dexterity, uint constitution, uint intelligence,
        uint wisdom, uint charisma)
    {
        return new AbilityScores(strength, dexterity, constitution, intelligence, wisdom, charisma);
    }
}