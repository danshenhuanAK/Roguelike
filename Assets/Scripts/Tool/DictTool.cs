using System.Collections.Generic;

public static class DictTool
{
    public static Tvalue GetValue<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {
        dict.TryGetValue(key, out Tvalue value);
        return value;
    }
}