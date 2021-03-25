using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubWnd<TSubWndMono> where TSubWndMono : SubWndMono
{
    protected abstract void OnLoad();
    protected abstract void OnShow();
    protected abstract void OnHide();
    protected abstract void OnDestroy();

    protected TSubWndMono Mono;
    private GameObject goWnd;

    protected bool IsShow = false;

    public SubWnd(string resName, Transform parent)
    {
        goWnd = Object.Instantiate(ResUtil.LoadPrefab(EPrefabType.Wnd, resName), parent);
        goWnd.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        Mono = goWnd.transform.GetComponent<TSubWndMono>();
        OnLoad();
    }

    public void Show()
    {
        if (IsShow || goWnd == null)
        {
            return;
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
