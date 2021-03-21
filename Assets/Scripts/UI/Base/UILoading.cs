using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EScene
{
    Start,
    Loading,
    Main,
}

public class UILoading : MonoBehaviour
{
    public EScene CurrentScene;

    // Start is called before the first frame update
    void Start()
    {
        switch (CurrentScene)
        {
            case EScene.Start:
                break;
            case EScene.Loading:
                break;
            case EScene.Main:
                MainUILayerMgr.Instance.Init();
                break;
        }
    }

}
