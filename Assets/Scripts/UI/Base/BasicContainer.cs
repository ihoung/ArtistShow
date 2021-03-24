using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicContainer<TUnit, TUnitMono> where TUnit : new() where TUnitMono : BasicUnitMono
{
    private List<TUnit> m_displayedUnits = new List<TUnit>();
    private List<TUnit> m_waitingUnits = new List<TUnit>();

}
