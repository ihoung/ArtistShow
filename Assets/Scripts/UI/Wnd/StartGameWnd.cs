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

    }

    protected override void OnHide()
    {

    }

    protected override void OnDestroy()
    {

    }

    private void ContinueGame()
    {
        LoadingMgr.Instance.LoadScene(EScene.Main);
    }

    private void StartNewGame()
    {
        LoadingMgr.Instance.LoadScene(EScene.Main);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
