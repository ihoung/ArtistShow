using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUILayer
{
    Base,
    Bottom,
    Middle,
    Top,
    OverAll,
}

public abstract class BasicWnd<TWndMono> where TWndMono : BasicWndMono
{
    protected abstract string ResName { get; }
    protected abstract EUILayer Layer { get; }

    protected abstract void OnInit();
    protected abstract void OnShow();
    protected abstract void OnHide();
    protected abstract void OnDestroy();

    protected TWndMono Mono;
    private GameObject goWnd;

    protected bool IsShow = false;

    private void Init()
    {
        goWnd = Object.Instantiate(ResUtil.LoadPrefab(EPrefabType.Wnd, ResName), UIMgr.Instance.UILayer[Layer]);

        RectTransform rectTrans = goWnd.GetComponent<RectTransform>();
        rectTrans.anchorMin = new Vector2(0f, 0f);
        rectTrans.anchorMax = new Vector2(1f, 1f);
        rectTrans.offsetMin = new Vector2(0f, 0f);
        rectTrans.offsetMax = new Vector2(0f, 0f);

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
        Object.Destroy(goWnd);
        OnDestroy();
    }
}
