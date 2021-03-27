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
        Mono.btnSetting.onClick.AddListener(OpenSettingPanel);

        ProgressMgr.Instance.OnStageBegin += RefreshProgress;
        EventMgr.Instance.OnEventRefreshed += RefreshEvent;

        PlayerDataMgr.Instance.CurPlayerData.BasicInfo.OnPropertyChg += RefreshProperty;
    }

    protected override void OnShow()
    {
        RefreshProgress();

        Mono.txtAssetValue.SetText(PlayerDataMgr.Instance.CurPlayerData.BasicInfo.Asset.ToString());
        Mono.txtConnectionValue.SetText(PlayerDataMgr.Instance.CurPlayerData.BasicInfo.Connection.ToString());
        Mono.txtStrengthValue.SetText(PlayerDataMgr.Instance.CurPlayerData.BasicInfo.Strength.ToString());
    }

    protected override void OnHide()
    {

    }

    protected override void OnDestroy()
    {
        ProgressMgr.Instance.OnStageBegin -= RefreshProgress;
        EventMgr.Instance.OnEventRefreshed -= RefreshEvent;
        PlayerDataMgr.Instance.CurPlayerData.BasicInfo.OnPropertyChg -= RefreshProperty;
    }

    public void RefreshProgress()
    {
        Mono.imgBg.SetSprite(ESpriteType.Bg, ProgressMgr.Instance.CurPeriod.BgName);

        if (ProgressMgr.Instance.CurPeriod.Dialog != 0)
            DialogWnd.Instance.ShowDialog(ProgressMgr.Instance.CurPeriod.Dialog);

        RefreshEvent();
    }

    public void RefreshEvent()
    {
        if (ProgressMgr.Instance.CurPeriod.Stage == EProgressStage.Preliminary || ProgressMgr.Instance.CurPeriod.Stage == EProgressStage.SemiFinal || ProgressMgr.Instance.CurPeriod.Stage == EProgressStage.Final)
        {
            Mono.txtNextProgress.SetText(TableMgr.Instance.TableUIString.GetFirstItem(item => item.Key == "HaveARest").Value);
        }
        else
        {
            Mono.txtNextProgress.SetText(TableMgr.Instance.TableUIString.GetFirstItem(item => item.Key == "GotoMatch").Value);
        }

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

    private void RefreshProperty(EPlayerPropType propType, int value)
    {
        switch (propType)
        {
            case EPlayerPropType.Asset:
                Mono.txtAssetValue.SetText(value.ToString());
                break;
            case EPlayerPropType.Connection:
                Mono.txtConnectionValue.SetText(value.ToString());
                break;
            case EPlayerPropType.Strength:
                Mono.txtStrengthValue.SetText(value.ToString());
                break;
        }
    }

    private void OpenSettingPanel()
    {
        SettingWnd.Instance.Show();
    }
}
