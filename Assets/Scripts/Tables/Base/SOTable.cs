using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SOTable<TSOTableItem> where TSOTableItem : SOTableItem
{
    public List<TSOTableItem> Items;
}
