﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SOTableItemAnswer : SOTableItem
{
    public string option;
    public int feedback;
}

[System.Serializable]
public class SOTableAnswer : SOTable<SOTableItemAnswer>
{

}