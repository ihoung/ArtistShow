using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemPeriod : TableItem<SOTableItemPeriod>
{
    public EProgressStage Stage { get; private set; }
    public string BgName { get; private set; }
    public List<int> Events { get; private set; }

    public override void Parse(SOTableItemPeriod SOData)
    {
        Stage = SOData.stage;
        BgName = SOData.bg_name;
        Events = ParseUtil.ParseIntList(SOData.events);
    }
}
