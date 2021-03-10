using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleTon<T> where T:new()
{
    private static readonly object m_lock = new object();
    protected static T m_instance = default(T);

    public abstract void Init();
    public abstract void Dispose();

    public static T Instance
    {
        get
        {
            lock (m_lock)
            {
                if (m_instance == null)
                {
                    if (m_instance == null)
                        m_instance = new T();
                }
                return m_instance;
            }
        }
    }
}


