using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table<TSOTable, TSOTableItem> where TSOTable : SOTable<TSOTableItem> where TSOTableItem : SOTableItem
{
    public Dictionary<int, TSOTableItem> m_dictTable = new Dictionary<int, TSOTableItem>();

    public TSOTableItem GetItem(int id)
    {
        TSOTableItem ret = default;
        if (m_dictTable.ContainsKey(id))
            ret = m_dictTable[id];
        return m_dictTable[id];
    }

    public List<TSOTableItem> GetRandomItem(Func<bool> conditionFunc)
    {
        List<TSOTableItem> ret = new List<TSOTableItem>();
        foreach (var p in m_dictTable)
        {
            if (conditionFunc())
                ret.Add(p.Value);
        }
        return ret;
    }
}
