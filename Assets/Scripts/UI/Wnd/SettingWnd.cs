using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingWnd : BaseSingleTonWnd<SettingWnd, SettingWndMono>
{
    protected override string ResName => "SettingWnd";

    protected override EUILayer Layer => EUILayer.OverAll;

    protected override void OnInit()
    {
        Mono.btnBack2Menu.onClick.AddListener(Back2Menu);
        Mono.btnSave.onClick.AddListener(SaveGame);
        Mono.btnClose.onClick.AddListener(Hide);
    }

    protected override void OnShow()
    {

    }

    protected override void OnHide()
    {

    }

    protected override void OnDestroy()
    {

    }

    private void Back2Menu()
    {
        GameRoot.Instance.Back2Menu();
    }

    private void SaveGame()
    {
        PlayerDataMgr.Instance.OnGameSaved += OnSaveSucc;
        PlayerDataMgr.Instance.SaveGame();
    }

    private void OnSaveSucc()
    {
        PlayerDataMgr.Instance.OnGameSaved -= OnSaveSucc;
        ConfirmWnd.Instance.ShowWnd("存档成功", TableMgr.GetUIString("Confirm"));
    }
}
