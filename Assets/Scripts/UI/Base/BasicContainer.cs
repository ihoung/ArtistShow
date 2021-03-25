using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicContainer<TUnit, TUnitMono, TContainerMono> where TUnit : BasicUnit<TUnitMono>, new() where TUnitMono : BasicUnitMono where TContainerMono : BasicContainerMono
{
    protected TContainerMono Mono; 

    private List<TUnit> m_displayedUnits = new List<TUnit>();
    private List<TUnit> m_waitingUnits = new List<TUnit>();

    public BasicContainer(TContainerMono mono)
    {
        Mono = mono;
    }

    public TUnit ShowUnit()
    {
        TUnit ret = new TUnit();

        if (m_waitingUnits.Count != 0)
        {
            ret = m_waitingUnits[0];
            m_waitingUnits.RemoveAt(0);
        }
        else
        {
            ret.Load(Mono.transform);
        }

        m_displayedUnits.Add(ret);

        return ret;
    }

    public void ClearUnit(TUnit unit)
    {
        m_displayedUnits.Remove(unit);
        m_waitingUnits.Add(unit);
    }

    public void ClearAllUnits()
    {
        foreach (var unit in m_displayedUnits)
        {
            m_waitingUnits.Remove(unit);
            unit.Hide();
        }
        m_displayedUnits.Clear();
    }
}
