using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBgWnd : BaseSingleTonWnd<MainBgWnd, MainBgWndMono>
{
    protected override string ResName => "MainBgWnd";

    protected override EUILayer Layer => EUILayer.Bottom;
    
    protected override void OnInit()
    {
        Mono.btnEvent.onClick.AddListener(TriggerEvent);
        Mono.btnNextProgress.onClick.AddListener(OnNextClicked);

        ProgressMgr.Instance.OnStageBegin += RefreshProgress;
        EventMgr.Instance.OnEventRefreshed += RefreshProgress;
    }

    protected override void OnShow()
    {
        RefreshProgress();
    }

    protected override void OnHide()
    {

    }

    protected override void OnDestroy()
    {
        ProgressMgr.Instance.OnStageBegin -= RefreshProgress;
        EventMgr.Instance.OnEventRefreshed -= RefreshProgress;
    }

    public void RefreshProgress()
    {
        Mono.imgBg.SetSprite(ESpriteType.Bg, ProgressMgr.Instance.CurPeriod.BgName);
        if (ProgressMgr.Instance.CurPeriod.Stage == EProgressStage.Preliminary || ProgressMgr.Instance.CurPeriod.Stage == EProgressStage.SemiFinal || ProgressMgr.Instance.CurPeriod.Stage == EProgressStage.Final)
        {
            Mono.txtNextProgress.SetText(TableMgr.Instance.TableUIString.GetFirstItem(item => item.Key == "HaveARest").Value);
        }
        else
        {
            Mono.txtNextProgress.SetText(TableMgr.Instance.TableUIString.GetFirstItem(item => item.Key == "GotoMatch").Value);
        }

        RefreshEvent();
    }

    public void RefreshEvent()
    {
        Mono.btnNextProgress.gameObject.SetActive(!EventMgr.Instance.HasEvent());
        Mono.btnEvent.gameObject.SetActive(EventMgr.Instance.HasEvent());
    }

    private void TriggerEvent()
    {
        EventMgr.Instance.TriggerEvent();
    }

    private void OnNextClicked()
    {
        ProgressMgr.Instance.Go2NextStage();
    }
}
