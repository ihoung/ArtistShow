using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUILayer
{
    Bottom,
}

public abstract class BaseWnd<TWndMono> where TWndMono : BaseWndMono
{
    private const string wndsPath = "Prefabs/Wnd/";

    protected abstract string ResName { get; }
    protected abstract EUILayer Layer { get; }

    protected abstract void OnInit();
    protected abstract void OnShow();
    protected abstract void OnHide();
    protected abstract void OnDestroy();

    public TWndMono Mono;
    private GameObject goWnd;

    protected bool IsShow = false;

    private void Init()
    {
        goWnd = Resources.Load<GameObject>(wndsPath + ResName);
        Object.Instantiate(goWnd, UILayerMgr.Instance.UILayer[Layer]);
        Mono = goWnd.transform.GetComponent<TWndMono>();
        OnInit();
    }

    public void Show()
    {
        if (IsShow)
        {
            return;
        }

        if (goWnd == null)
        {
            Init();
        }

        goWnd.SetActive(true);
        IsShow = true;
        OnShow();
    }

    public void Hide()
    {
        goWnd.SetActive(false);
        IsShow = false;
        OnHide();
    }

    public void Destroy()
    {
        IsShow = false;
        Mono = null;
        goWnd = null;
        OnDestroy();
    }
}
