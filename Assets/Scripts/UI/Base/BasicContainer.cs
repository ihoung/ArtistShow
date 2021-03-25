using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicContainer<TUnit, TUnitMono, TContainerMono> where TUnit : BasicUnit<TUnitMono>, new() where TUnitMono : BasicUnitMono where TContainerMono : BasicContainerMono
{
    protected TContainerMono Mono; 

    private Queue<TUnit> m_displayedUnits = new Queue<TUnit>();
    private Queue<TUnit> m_waitingUnits = new Queue<TUnit>();

    public BasicContainer(TContainerMono mono)
    {
        Mono = mono;
    }

    public TUnit ShowUnit()
    {
        TUnit ret = new TUnit();

        if (m_waitingUnits.Count != 0)
        {
            ret = m_waitingUnits.Dequeue();
        }
        else
        {
            ret.Load(Mono.transform);
        }

        m_displayedUnits.Enqueue(ret);

        return ret;
    }

    public void ClearAllUnits()
    {
        foreach (var unit in m_displayedUnits)
        {
            m_waitingUnits.Enqueue(unit);
        }
        m_displayedUnits.Clear();
    }
}
