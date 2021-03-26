using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EOptionType
{
    Quiz,
    Guessing,
}

public class OptionWnd : BaseSingleTonWnd<OptionWnd, OptionWndMono>
{
    protected override string ResName => "OptionWnd";

    protected override EUILayer Layer => EUILayer.Top;

    private EOptionType m_type;
    private TableItemQuiz m_curData;

    protected override void OnInit()
    {

    }

    protected override void OnShow()
    {
        Mono.imgObject.gameObject.SetActive(m_type == EOptionType.Guessing);
        switch (m_curData.ID)
        {
            case 6:
                Mono.imgObject.SetSprite(ESpriteType.Guessing, "treasure_1");
                break;
            case 7:
                Mono.imgObject.SetSprite(ESpriteType.Guessing, "treasure_2");
                break;
            case 8:
                Mono.imgObject.SetSprite(ESpriteType.Guessing, "treasure_3");
                break;
        }

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

    public void ShowWnd(EOptionType type, int id)
    {
        m_curData = TableMgr.Instance.TableQuiz.GetItem(id);
        m_type = type;

        Show();
    }

    private void OnOptionSelected()
    {
        Hide();
    }
}
