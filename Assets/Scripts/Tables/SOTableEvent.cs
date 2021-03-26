using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEventType
{
    Quiz,
    Guessing,
    Painting,
}

[System.Serializable]
public class SOTableItemEvent : SOTableItem
{
    public EEventType type;
    public int param;
}

[System.Serializable]
public class SOTableEvent : SOTable<SOTableItemEvent>
{

}
