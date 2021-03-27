using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMgr : MonoSingleTon<LoadingMgr>
{
    public override void Init()
    {

    }

    public override void Dispose()
    {

    }

    private string m_curScene;

    public void SwitchScene(EScene scene)
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

        StartCoroutine("LoadScene");       
    }

    private IEnumerator LoadScene()
    {
        var asycOp = SceneManager.LoadSceneAsync(m_curScene);
        asycOp.allowSceneActivation = false;
        while (asycOp.progress < 1f)
        {
            yield return new WaitForEndOfFrame();
        }
        asycOp.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        OnLoadCompleted();
        yield break;
    }

    private void OnLoadCompleted()
    {
        switch (m_curScene)
        {
            case "Main":
                GameRoot.Instance.EnterGame();
                break;
            case "Start":
                UIMgr.Instance.InitNewScene(EScene.Start);
                break;
        }
    }
}
