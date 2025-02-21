namespace SuperDungeons.Utils;

internal class DoubleMapValue<TV, TW>(TV value1, TW value2)
{
    public TV Value1 { get; set; } = value1;
    public TW Value2 { get; set; } = value2;
}