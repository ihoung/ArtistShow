using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTable : Table<SOTestTable, SOTestTableItem>
{

}

public class SOTestTableItem : SOTableItem
{
    public int pTest;
}

public class SOTestTable : SOTable<SOTestTableItem>
{

}


