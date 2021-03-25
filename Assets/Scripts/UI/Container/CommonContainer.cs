using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonContainer<TUnit, TUnitMono> : BasicContainer<TUnit, TUnitMono, CommonContainerMono> where TUnit : BasicUnit<TUnitMono>, new() where TUnitMono : BasicUnitMono
{
    public CommonContainer(CommonContainerMono mono) : base(mono) { }
}
