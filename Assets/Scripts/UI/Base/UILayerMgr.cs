using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : SingleTon<UIMgr>
{
    public RectTransform Base;
    public RectTransform Bottom;
    public RectTransform Middle;
    public RectTransform Top;
    public RectTransform OverAll;

    public Dictionary<EUILayer, RectTransform> UILayer { get; private set; } = new Dictionary<EUILayer, RectTransform>();

    private EScene m_curScene;

    public override void Init()
    {
        InitNewScene(EScene.Start);
    }

    public override void Dispose()
    {

    }

    public void InitNewScene(EScene scene)
    {
        if (m_curScene == scene)
            return;

        m_curScene = scene;
        InitSceneUI(scene);
    }

    private void InitSceneUI(EScene scene)
    {
        UILayer.Clear();

        UIRoot root = Resources.FindObjectsOfTypeAll<UIRoot>()[0];
        UILayer.Add(EUILayer.Base, root.Base);

        switch (m_curScene)
        {
            case EScene.Start:
                StartGameWnd.Instance.Show();
                break;
            case EScene.Loading:
                break;
            case EScene.Main:
                UILayer.Add(EUILayer.Bottom, root.Bottom);
                UILayer.Add(EUILayer.Middle, root.Middle);
                UILayer.Add(EUILayer.Top, root.Top);
                UILayer.Add(EUILayer.OverAll, root.OverAll);
                break;
        }
    }
}
