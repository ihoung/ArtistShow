using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SOTableItemFeedback : SOTableItem
{
    public int asset;
    public int connection;
    public int strength;
}

[System.Serializable]
public class SOTableFeedback : SOTable<SOTableItemFeedback>
{

}
