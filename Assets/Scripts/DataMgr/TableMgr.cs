using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMgr : SingleTon<TableMgr>
{
    public Table<TableItemUIString, SOTableUIString, SOTableItemUIString> TableUIString;
    public Table<TableItemDialog, SOTableDialog, SOTableItemDialog> TableDialog;

    public override void Init()
    {
        LoadTable();
    }

    public override void Dispose()
    {
        TableUIString = default;
        TableDialog = default;
    }

    private void LoadTable()
    {
        TableUIString = new Table<TableItemUIString, SOTableUIString, SOTableItemUIString>(ResUtil.LoadSO<SOTableUIString>(typeof(SOTableUIString).Name));
        TableDialog = new Table<TableItemDialog, SOTableDialog, SOTableItemDialog>(ResUtil.LoadSO<SOTableDialog>(typeof(SOTableDialog).Name));
    }
}
