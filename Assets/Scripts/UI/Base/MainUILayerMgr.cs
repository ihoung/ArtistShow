using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUILayerMgr : MonoSingleTon<MainUILayerMgr>
{
    public RectTransform Bottom;
    public RectTransform Middle;
    public RectTransform Top;
    public RectTransform OverAll;

    public Dictionary<EUILayer, RectTransform> UILayer { get; private set; }

    public override void Init()
    {
        Bottom = GameObject.Find("Bottom").GetComponent<RectTransform>();
        Middle = GameObject.Find("Middle").GetComponent<RectTransform>();
        Top = GameObject.Find("Top").GetComponent<RectTransform>();
        OverAll = GameObject.Find("OverAll").GetComponent<RectTransform>();
        UILayer = new Dictionary<EUILayer, RectTransform>
        {
            { EUILayer.Bottom, m_instance.Bottom }, { EUILayer.Middle, m_instance.Middle }, { EUILayer.Top, m_instance.Top }, { EUILayer.OverAll, m_instance.OverAll },
        };
    }

    public override void Dispose()
    {

    }

}
