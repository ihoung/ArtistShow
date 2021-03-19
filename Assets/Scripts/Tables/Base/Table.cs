using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Table<TSOTable, TSOTableItem> where TSOTable : SOTable<TSOTableItem> where TSOTableItem : SOTableItem
{
    public string ResName { get; }

    private Dictionary<int, TSOTableItem> m_dictTable = new Dictionary<int, TSOTableItem>();

    public Table(TSOTable so)
    {
        foreach(var item in so.Items)
        {
            m_dictTable.Add(item.ID, item);
        }
    }

    public TSOTableItem GetItem(int id)
    {
        TSOTableItem ret = default;
        if (m_dictTable.ContainsKey(id))
            ret = m_dictTable[id];
        return m_dictTable[id];
    }

    public List<TSOTableItem> GetItems(Func<bool> conditionFunc)
    {
        List<TSOTableItem> ret = new List<TSOTableItem>();
        foreach (var p in m_dictTable)
        {
            if (conditionFunc())
                ret.Add(p.Value);
        }
        return ret;
    }

    public List<TSOTableItem> GetAllItems()
    {
        return m_dictTable.Values.ToList();
    }
}
