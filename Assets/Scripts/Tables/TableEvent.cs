using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemEvent : TableItem<SOTableItemEvent>
{
    public EEventType Type { get; private set; }
    public int Param { get; private set; }

    public override void Parse(SOTableItemEvent SOData)
    {
        Param = SOData.param;
        Type = SOData.type;
    }
}
