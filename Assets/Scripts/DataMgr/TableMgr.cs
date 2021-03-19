using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMgr : SingleTon<TableMgr>
{
    public Table<TableItemDialog, SOTableDialog, SOTableItemDialog> TableDialog;

    public override void Init()
    {
        LoadTable();
    }

    public override void Dispose()
    {
        TableDialog = default;
    }

    private void LoadTable()
    {
        TableDialog = new Table<TableItemDialog, SOTableDialog, SOTableItemDialog>(ResUtil.LoadSO<SOTableDialog>(typeof(SOTableDialog).Name));
    }
}
