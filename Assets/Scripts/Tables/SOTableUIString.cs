using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SOTableItemUIString : SOTableItem
{
    public string key;
    public string value;
}

[System.Serializable]
public class SOTableUIString : SOTable<SOTableItemUIString>
{

}
