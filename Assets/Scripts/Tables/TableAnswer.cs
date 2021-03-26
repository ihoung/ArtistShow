using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemAnswer : TableItem<SOTableItemAnswer>
{
    public string Content;
    public int FeedbackID;

    public override void Parse(SOTableItemAnswer SOData)
    {
        Content = SOData.option;
        FeedbackID = SOData.feedback;
    }
}
