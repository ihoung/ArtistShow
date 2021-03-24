using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSingleTonWnd<TWnd, TWndMono> : BasicWnd<TWndMono> where TWnd : new() where TWndMono : BasicWndMono
{
    private static readonly object m_lock = new object();
    protected static TWnd m_instance = default;

    public static TWnd Instance
    {
        get
        {
            lock (m_lock)
            {
                if (m_instance == null)
                {
                    if (m_instance == null)
                        m_instance = new TWnd();
                }
                return m_instance;
            }
        }
    }
}
