/*
namespace SuperDungeons.Model.Resources;

public class ResourceManager
{
    private HashSet<Resource> _resources = [];

    public void AddResource(Resource resource)
    {
        _resources.Add(resource);
    }

    public void RemoveResource(string name)
    {
        //because for resources equality is only determined by name, this will work
        _resources.Remove(new Resource(name, 0, RechargeTrigger.Never));
    }

    public List<string> GetAllResourceNames()
    {
        return _resources.Select(r => r.Name).ToList();
    }
}
*/