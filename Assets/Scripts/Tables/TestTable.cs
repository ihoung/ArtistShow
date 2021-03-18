using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETest
{
    AAA,
    BBB,
}

public class TableTest : Table<SOTableTest, SOTableItemTest>
{

}

public class SOTableItemTest : SOTableItem
{
    public int p1;
    public float p2;
    public double p3;
    public string p4;
    public ETest p5;
}

public class SOTableTest : SOTable<SOTableItemTest>
{

}


