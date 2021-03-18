using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public interface ISOTable
{
#if UNITY_EDITOR
    void Json2SO(string json);
#endif
}

[System.Serializable]
public class SOTable<TSOTableItem> : ISOTable where TSOTableItem : SOTableItem
{
    public TSOTableItem[] Items;

#if UNITY_EDITOR
    public void Json2SO(string json)
    {
        try
        {
            Items = JsonConvert.DeserializeObject<TSOTableItem[]>(json);
        }
        catch (Exception e)
        {
            Debug.Log("Table : " + this.GetType().Name + " format has error :" + e);
        }
    }
#endif
}
