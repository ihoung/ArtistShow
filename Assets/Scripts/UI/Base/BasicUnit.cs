using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicUnit<TUnitMono> where TUnitMono : BasicUnitMono
{
    protected TUnitMono Mono;

    protected abstract string ResName { get; }
    protected virtual void OnBind()
    {

    }

    public void Load(Transform parent)
    {
        var res = ResUtil.LoadPrefab(EPrefabType.Unit, ResName);
        Mono = Object.Instantiate(res, parent) as TUnitMono;
    }

    public void Bind(TUnitMono mono)
    {
        Mono = mono;
        OnBind();
    }
}
