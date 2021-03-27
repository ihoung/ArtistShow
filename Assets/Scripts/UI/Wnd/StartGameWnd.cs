using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameWnd : BaseSingleTonWnd<StartGameWnd, StartGameWndMono>
{
    protected override string ResName => "StartGameWnd";

    protected override EUILayer Layer => EUILayer.Base;

    protected override void OnInit()
    {
        Mono.btnContinue.onClick.AddListener(ContinueGame);
        Mono.btnNewGame.onClick.AddListener(StartNewGame);
        Mono.btnQuit.onClick.AddListener(QuitGame);
    }

    protected override void OnShow()
    {
        Mono.btnContinue.gameObject.SetActive(ArchiveUtil.ArchiveExist());

    }

    protected override void OnHide()
    {

    }

    protected override void OnDestroy()
    {

    }

    private void ContinueGame()
    {
        Hide();

        GameRoot.Instance.SetLoadingMode(ELoadingMode.Continue);
        LoadingMgr.Instance.SwitchScene(EScene.Main);
    }

    private void StartNewGame()
    {
        Hide();

        GameRoot.Instance.SetLoadingMode(ELoadingMode.NewGame);
        LoadingMgr.Instance.SwitchScene(EScene.Main);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
