namespace SuperDungeons.Utils;

public class DoubleMap<TK, TV, TW> where TK : notnull
{

    private readonly Dictionary<TK, DoubleMapValue<TV, TW>> _map = [];

    public void Add(TK key, TV value1, TW value2)
    {
        _map.Add(key, new DoubleMapValue<TV, TW>(value1, value2));
    }

    public TV GetValue1(TK key)
    {
        return _map[key].Value1;
    }
    
    public TW GetValue2(TK key)
    {
        return _map[key].Value2;
    }
}