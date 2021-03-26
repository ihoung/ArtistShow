using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TableItem<TSOTableItem> where TSOTableItem : SOTableItem
{
    public int ID;
    public abstract void Parse(TSOTableItem SOData);

    public void CreateInstance(TSOTableItem soItem)
    {
        ID = soItem.ID;
        Parse(soItem);
    }
}

public class Table<TTableItem, TSOTable, TSOTableItem> where TTableItem : TableItem<TSOTableItem>, new() where TSOTable : SOTable<TSOTableItem> where TSOTableItem : SOTableItem
{
    public string ResName { get; }

    private readonly Dictionary<int, TTableItem> m_dictTable = new Dictionary<int, TTableItem>();

    public Table(TSOTable so)
    {
        foreach (var soItem in so.Items)
        {
            TTableItem item = new TTableItem();
            item.CreateInstance(soItem);
            m_dictTable.Add(item.ID, item);
        }
    }

    public TTableItem GetItem(int id)
    {
        if (m_dictTable.ContainsKey(id))
            return m_dictTable[id];

        return null;
    }

    public List<TTableItem> GetItems(Func<bool> conditionFunc)
    {
        List<TTableItem> ret = new List<TTableItem>();
        foreach (var p in m_dictTable)
        {
            if (conditionFunc())
                ret.Add(p.Value);
        }
        return ret;
    }

    public TTableItem GetFirstItem(Func<TTableItem, bool> conditionFunc)
    {
        foreach (var item in m_dictTable.Values)
        {
            if (conditionFunc(item))
                return item;
        }
        return null;
    }

    public List<TTableItem> GetAllItems()
    {
        return m_dictTable.Values.ToList();
    }
}
