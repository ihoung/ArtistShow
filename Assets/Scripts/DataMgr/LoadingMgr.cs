using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ELoadingMode
{
    NewGame,
    Continue,
}

public class LoadingMgr : MonoSingleTon<LoadingMgr>
{
    public override void Init()
    {

    }

    public override void Dispose()
    {

    }

    private AsyncOperation m_asycOp;
    private string m_curScene;

    private void LoadScene(EScene scene)
    {
        SceneManager.LoadScene("Loading");

        switch (scene)
        {
            case EScene.Start:
                m_curScene = "Start";
                break;
            case EScene.Main:
                m_curScene = "Main";
                break;
        }

        m_asycOp = SceneManager.LoadSceneAsync(m_curScene, LoadSceneMode.Additive);
        m_asycOp.allowSceneActivation = false;
    }

    public void EnterGame(ELoadingMode mode)
    {
        LoadScene(EScene.Main);

        switch (mode)
        {
            case ELoadingMode.NewGame:
                PlayerDataMgr.Instance.OnGameCreated += OnGameCreatedCompleted;
                PlayerDataMgr.Instance.CreateNewArchive();
                break;
            case ELoadingMode.Continue:
                PlayerDataMgr.Instance.OnGameLoaded += OnGameLoadedCompleted;
                PlayerDataMgr.Instance.LoadGame();
                break;
        }
    }

    private void OnGameLoadedCompleted()
    {
        PlayerDataMgr.Instance.OnGameLoaded -= OnGameLoadedCompleted;
        CloseLoadingScene();
    }

    private void OnGameCreatedCompleted()
    {
        PlayerDataMgr.Instance.OnGameCreated -= OnGameCreatedCompleted;
        CloseLoadingScene();
    }

    private void CloseLoadingScene()
    {
        m_asycOp.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync("Loading");
    }
}
