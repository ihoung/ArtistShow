using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class BasicUnit<TUnitMono> where TUnitMono : BasicUnitMono
{
    protected TUnitMono Mono;
    private GameObject goUnit;
    private bool isActive;

    protected abstract string ResName { get; }
    protected abstract void OnInit();

    public void Load(Transform parent)
    {
        var res = ResUtil.LoadPrefab(EPrefabType.Unit, ResName);
        goUnit = UnityEngine.Object.Instantiate(res, parent);
        Mono = goUnit.GetComponent<TUnitMono>();
        isActive = true;
        OnInit();
    }

    public void Bind(TUnitMono mono)
    {
        Mono = mono;
        isActive = true;
        OnInit();
    }

    public void Show()
    {
        if (isActive)
            return;
        isActive = true;
        goUnit.SetActive(true);
    }

    public void Hide()
    {
        isActive = false;
        goUnit.SetActive(false);
    }
}
