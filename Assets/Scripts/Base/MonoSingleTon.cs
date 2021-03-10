using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    private static readonly object m_lock = new object();
    protected static T m_instance = default(T);

    protected static bool IsApplicationQuit { get; private set; }

    public abstract void Init();
    public abstract void Dispose();

    public static T Instance
    {
        get
        {
            if (IsApplicationQuit)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.LogWarning("[Singleton] " + typeof(T) + " already destroyed on application quit." + " Won't create again - returning null.");
                }

                return default(T);
            }
            else
            {
                lock (m_lock)
                {
                    if (m_instance == null)
                    {
                        T[] objects = FindObjectsOfType<T>();
                        if (objects.Length != 0)
                        {
                            if (objects.Length > 1)
                            {
                                if (Debug.isDebugBuild)
                                {
                                    Debug.LogWarning("[Singleton] " + typeof(T).Name + " should never be more than 1 in scene!");
                                }
                            }

                            m_instance = objects[0];
                        }
                        else
                        {

                            GameObject singletonObj = new GameObject();
                            m_instance = singletonObj.AddComponent<T>();
                            singletonObj.name = typeof(T) + "[MonoMgr]";

                            if (Application.isPlaying)
                            {
                                DontDestroyOnLoad(singletonObj);
                            }
                        }
                    }

                    return m_instance;
                }
            }
        }
    }
}
