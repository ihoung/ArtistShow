using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OptionUnit : BasicUnit<OptionUnitMono>
{
    protected override string ResName => "OptionUnit";

    private TableItemAnswer m_data;
    private Action m_onClicked;

    protected override void OnInit()
    {
        Mono.btnSelect.onClick.AddListener(OnSelected);
    }

    public void SetData(TableItemAnswer data, Action onClick)
    {
        m_data = data;
        m_onClicked = onClick;

        Mono.txtOption.SetText(data.Content);
    }

    private void OnSelected()
    {
        PlayerDataMgr.Instance.CurPlayerData.BasicInfo.DoFeedBack(TableMgr.Instance.TableFeedback.GetItem(m_data.FeedbackID));

        m_onClicked?.Invoke();
    }
}
