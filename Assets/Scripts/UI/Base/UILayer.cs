using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayerMgr : MonoSingleTon<UILayerMgr>
{
    public RectTransform Bottom;

    public Dictionary<EUILayer, RectTransform> UILayer { get; private set; }

    public override void Init()
    {
        Bottom = GameObject.Find("Bottom").GetComponent<RectTransform>();
        UILayer = new Dictionary<EUILayer, RectTransform> { { EUILayer.Bottom, m_instance.Bottom } };
    }

    public override void Dispose()
    {
    }

}
