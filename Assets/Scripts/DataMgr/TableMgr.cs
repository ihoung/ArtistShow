using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMgr : SingleTon<TableMgr>
{
    public Table<TableItemUIString, SOTableUIString, SOTableItemUIString> TableUIString;
    public Table<TableItemDialog, SOTableDialog, SOTableItemDialog> TableDialog;
    public Table<TableItemPeriod, SOTablePeriod, SOTableItemPeriod> TablePeriod;
    public Table<TableItemEvent, SOTableEvent, SOTableItemEvent> TableEvent;
    public Table<TableItemQuiz, SOTableQuiz, SOTableItemQuiz> TableQuiz;
    public Table<TableItemAnswer, SOTableAnswer, SOTableItemAnswer> TableAnswer;
    public Table<TableItemFeedback, SOTableFeedback, SOTableItemFeedback> TableFeedback;
    public Table<TableItemEnding, SOTableEnding, SOTableItemEnding> TableEnding;

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
        TablePeriod = new Table<TableItemPeriod, SOTablePeriod, SOTableItemPeriod>(ResUtil.LoadSO<SOTablePeriod>(typeof(SOTablePeriod).Name));
        TableEvent = new Table<TableItemEvent, SOTableEvent, SOTableItemEvent>(ResUtil.LoadSO<SOTableEvent>(typeof(SOTableEvent).Name));
        TableQuiz = new Table<TableItemQuiz, SOTableQuiz, SOTableItemQuiz>(ResUtil.LoadSO<SOTableQuiz>(typeof(SOTableQuiz).Name));
        TableAnswer = new Table<TableItemAnswer, SOTableAnswer, SOTableItemAnswer>(ResUtil.LoadSO<SOTableAnswer>(typeof(SOTableAnswer).Name));
        TableFeedback = new Table<TableItemFeedback, SOTableFeedback, SOTableItemFeedback>(ResUtil.LoadSO<SOTableFeedback>(typeof(SOTableFeedback).Name));
        TableEnding = new Table<TableItemEnding, SOTableEnding, SOTableItemEnding>(ResUtil.LoadSO<SOTableEnding>(typeof(SOTableEnding).Name));
    }

    public static string GetUIString(string UIKey)
    {
        var data = Instance.TableUIString.GetFirstItem(item => item.Key == UIKey);
        return data.Value;
    }
}
