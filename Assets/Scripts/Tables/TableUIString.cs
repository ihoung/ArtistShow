using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemUIString : TableItem<SOTableItemUIString>
{
    public string Key;
    public string Value;

    public override void Parse(SOTableItemUIString SOData)
    {
        Key = SOData.key;
        Value = SOData.value;
    }
}
