using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EScene
{
    None = 0,
    Start,
    Loading,
    Main,
}

public class UIRoot : MonoBehaviour
{
    public EScene CurrentScene;

    public RectTransform Base { get; private set; }
    public RectTransform Bottom;
    public RectTransform Middle;
    public RectTransform Top;
    public RectTransform OverAll;

    private void Awake()
    {
        Base = transform as RectTransform;
    }

    void Start()
    {

    }

}
