using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionWnd : BaseSingleTonWnd<OptionWnd, OptionWndMono>
{
    protected override string ResName => "OptionWnd";

    protected override EUILayer Layer => EUILayer.Top;

    private TableItemQuiz m_curData;

    protected override void OnInit()
    {

    }

    protected override void OnShow()
    {
        Mono.optionContainer.ClearAllUnits();
        foreach(var optionId in m_curData.Options)
        {
            var data = TableMgr.Instance.TableAnswer.GetItem(optionId);
            var unit = Mono.optionContainer.ShowUnit();
            unit.SetData(data, OnOptionSelected);
        }

        Mono.txtQuestion.SetText(m_curData.Question);
    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {

    }

    public void ShowWnd(int id)
    {
        m_curData = TableMgr.Instance.TableQuiz.GetItem(id);

        Show();
    }

    private void OnOptionSelected(TableItemAnswer data)
    {
        Hide();
    }
}
