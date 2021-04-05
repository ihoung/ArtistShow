using System.Collections;
using System.Collections.Generic;
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

    private AsyncOperation m_asycOp;
    private string m_curScene;

    public void LoadScene(EScene scene)
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

        StartCoroutine("ILoadScene");
    }

    private IEnumerator ILoadScene()
    {
        m_asycOp = SceneManager.LoadSceneAsync(m_curScene, LoadSceneMode.Additive);
        m_asycOp.allowSceneActivation = false;
        while (m_asycOp.progress < 1f)
        {
            yield return new WaitForEndOfFrame();
        }
        m_asycOp.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        SceneManager.UnloadSceneAsync("Loading");
    }
}
