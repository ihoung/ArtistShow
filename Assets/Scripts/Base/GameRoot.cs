﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELoadingMode
{
    NewGame,
    Continue,
}

public class GameRoot : MonoSingleTon<GameRoot>
{
    private ELoadingMode m_curMode;

    public override void Init()
    {

    }

    public override void Dispose()
    {

    }

    public void SetLoadingMode(ELoadingMode mode)
    {
        m_curMode = mode;
    }

    public void EnterGame()
    {
        UIMgr.Instance.InitNewScene(EScene.Main);

        switch (m_curMode)
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
        OnEnterGame();
        
    }

    private void OnGameCreatedCompleted()
    {
        PlayerDataMgr.Instance.OnGameCreated -= OnGameCreatedCompleted;
        OnEnterGame();
    }

    private void OnEnterGame()
    {
        // 初始化Manager
        ProgressMgr.Instance.Init();
        EventMgr.Instance.Init();

        //初始窗口
        MainBgWnd.Instance.Show();
    }
}
