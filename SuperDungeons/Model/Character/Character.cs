using SuperDungeons.Model.Abilities;
using SuperDungeons.Model.Classes;

namespace SuperDungeons.Model.Character;

//This class is more of a Data Structure than anything else, it only exposes its components, so it causes coupling
//between users of this class and the components they use. As this is only a DataStructure that is okay,exposing all
//the necessary methods directly via this class would cause a really long class and be far less maintainable


public sealed class Character : BindableObject
{
    public Character()
    {
        //temp values for testing
        //ToDo make a characterBuilder or something to make a character with sensible values
        Overview = new CharacterOverview("", this);
        AbilityScores = new AbilityScores(10, 10, 10, 10, 10, 10);
        ClassManager = new();
    }

    public CharacterOverview Overview { get; }
    public AbilityScores AbilityScores { get; }
    public ClassManager ClassManager { get; }
}