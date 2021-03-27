using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EProgressStage
{
    Preliminary = 0,
    Rest_1,
    SemiFinal,
    Rest_2,
    Final,
}

[System.Serializable]
public class SOTableItemPeriod : SOTableItem
{
    public EProgressStage stage;
    public string bg_name;
    public string events;
    public int dialog;
}

[System.Serializable]
public class SOTablePeriod : SOTable<SOTableItemPeriod>
{

}
