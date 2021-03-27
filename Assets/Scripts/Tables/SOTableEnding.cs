using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SOTableItemEnding : SOTableItem
{
    public int dialog;
}

[System.Serializable]
public class SOTableEnding : SOTable<SOTableItemEnding>
{

}
