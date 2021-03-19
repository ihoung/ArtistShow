using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ResUtil
{
    private static readonly string soPath = "Tables/";
    private static readonly string imgPath = "Images/";
    private static readonly string wndsPath = "Prefabs/Wnd/";

    public static TSO LoadSO<TSO>(string resName) where TSO : ScriptableObject
    {
        TSO so = Resources.Load<TSO>(soPath + resName);
        if (so == null)
            Debug.LogError($"Load scriptobject {resName} failed!");
        return so;
    }

    public static Sprite LoadSprite(string resName)
    {
        if (string.IsNullOrEmpty(resName))
            return null;

        Sprite sp = Resources.Load<Sprite>(imgPath + resName);
        if (sp == null)
            Debug.LogError($"Load image {resName} failed!");
        return sp;
    }

    public static GameObject LoadPrefab(string resName)
    {
        GameObject go = Resources.Load<GameObject>(wndsPath + resName);
        if (go == null)
            Debug.LogError($"Load Prefab {resName} failed!");
        return go;
    }
}
