using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemEnding : TableItem<SOTableItemEnding>
{
    public int Dialog { get; private set; }

    public override void Parse(SOTableItemEnding SOData)
    {
        Dialog = SOData.dialog;
    }
}
