using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemFeedback : TableItem<SOTableItemFeedback>
{
    public int Asset { get; private set; }
    public int Connection { get; private set; }
    public int Strength { get; private set; }

    public override void Parse(SOTableItemFeedback SOData)
    {
        Asset = SOData.asset;
        Connection = SOData.connection;
        Strength = SOData.strength;
    }
}
