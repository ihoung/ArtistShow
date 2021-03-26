using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemQuiz : TableItem<SOTableItemQuiz>
{
    public string Question { get; private set; }
    public List<int> Options { get; private set; }

    public override void Parse(SOTableItemQuiz SOData)
    {
        Question = SOData.question;
        Options = ParseUtil.ParseIntList(SOData.options);
    }
}
