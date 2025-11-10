using SuperDungeons.Model.Abilities;
using SuperDungeons.Model.Classes;
using SuperDungeons.Model.Features;
using SuperDungeons.Utils;

namespace SuperDungeons.Model.Character;

//This class is more of a Data Structure than anything else, it only exposes its components, so it causes coupling
//between users of this class and the components they use. As this is only a DataStructure that is okay, exposing all
//the necessary methods directly via this class would cause a really long class and be far less maintainable


public sealed class Character : BindableObject
{
    private bool _hasInspiration;
    private string _name = "";

    public Character()
    {
        _classRepository = new ClassRepository();
        _choiceManager = new ChoiceManager();
        AbilityScores = new AbilityScores(10, 10, 10, 10, 10, 10);
        ClassManager = new ClassManager(_classRepository, _choiceManager);
    }
    
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly ClassRepository _classRepository;
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly ChoiceManager _choiceManager;
    
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public bool HasInspiration
    {
        get => _hasInspiration;
        set
        {
            _hasInspiration = value;
            OnPropertyChanged(nameof(HasInspiration));
        }
    }

    //ToDo make a characterBuilder or something to make a character with sensible values
    
    public IAbilityScores AbilityScores { get; }
    public ClassManager ClassManager { get; }
}