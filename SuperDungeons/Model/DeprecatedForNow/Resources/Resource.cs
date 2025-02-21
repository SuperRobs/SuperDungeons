/*
namespace SuperDungeons.Model.Resources;

public class Resource
{
    public Resource(string name, uint maxCharges, RechargeTrigger rechargeTrigger)
    {
        Name = name;
        MaxCharges = maxCharges;
        ChargesLeft = maxCharges;
        //depending on the rechargeTrigger, subscribe the Refresh Method to the appropriate event
        switch(rechargeTrigger)
        {
            case RechargeTrigger.Round:
                break;
            case RechargeTrigger.ShortRest:
                break;
            case RechargeTrigger.LongRest:
                break;
            case RechargeTrigger.Day:
                break;
            case RechargeTrigger.Never:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(rechargeTrigger), rechargeTrigger, 
                    "Resource did not expect " + rechargeTrigger + "was a new option added lately?");
        }
    }
    
    public string Name { get; }
    public uint MaxCharges { get; }
    public uint ChargesLeft { get; set; }

    public bool Use(uint amount)
    {
        if (ChargesLeft < amount)
        {
            return false;
        }

        ChargesLeft -= amount;
        return true;
    }

    public void Refresh()
    {
        ChargesLeft = MaxCharges;
    }

    public new bool Equals(object? o)
    {
        if (o is Resource other)
        {
            
            return Name.Equals(other.Name);
        }
        return false;
    }
}
*/