using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMgr : SingleTon<TableMgr>
{
    public Table<SOTableTest, SOTableItemTest> TableTest;

    public override void Init()
    {
        LoadTable();
    }

    public override void Dispose()
    {

    }

    private void LoadTable()
    {
        TableTest = new Table<SOTableTest, SOTableItemTest>(ResUtil.LoadSO<SOTableTest>(typeof(SOTableTest).Name));
    }
}
