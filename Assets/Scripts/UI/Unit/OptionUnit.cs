using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OptionUnit : BasicUnit<OptionUnitMono>
{
    protected override string ResName => "OptionUnit";

    private TableItemAnswer m_data;
    private Action<TableItemAnswer> m_onClicked;

    protected override void OnInit()
    {
        Mono.btnSelect.onClick.AddListener(OnSelected);
    }

    public void SetData(TableItemAnswer data, Action<TableItemAnswer> onClick)
    {
        m_data = data;
        m_onClicked = onClick;

        Mono.txtOption.SetText(data.Content);
    }

    private void OnSelected()
    {
        var feedBackData = TableMgr.Instance.TableFeedback.GetItem(m_data.FeedbackID);
        PlayerDataMgr.Instance.CurPlayerData.BasicInfo.DoFeedBack(feedBackData);
        ConfirmWnd.Instance.ShowWnd(PlayerBasicInfo.GetInfoStr(feedBackData), TableMgr.GetUIString("Confirm"));

        m_onClicked?.Invoke(m_data);
    }
}
