using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public enum ESpriteType
{
    Bg,
    Character,
    UI,
    Guessing,
}

public enum EPrefabType
{
    Wnd,
    Unit,
}

public static class ResUtil
{
    private static readonly string soPath = "Tables/";
    private static readonly string imgPath = "Images/";
    private static readonly string prefabPath = "Prefabs/";

    public static TSO LoadSO<TSO>(string resName) where TSO : ScriptableObject
    {
        TSO so = Resources.Load<TSO>(soPath + resName);
        if (so == null)
            Debug.LogError($"Load scriptobject {resName} failed!");
        return so;
    }

    public static Sprite LoadSprite(ESpriteType spType, string resName)
    {
        if (string.IsNullOrEmpty(resName))
            return null;

        string path = "";
        switch (spType)
        {
            case ESpriteType.Bg:
                path = Path.Combine(imgPath, "Bg", resName);
                break;
            case ESpriteType.Character:
                path = Path.Combine(imgPath, "Characters", resName);
                break;
            case ESpriteType.UI:
                path = Path.Combine(imgPath, "UI", resName);
                break;
            case ESpriteType.Guessing:
                path = Path.Combine(imgPath, "Guessing", resName);
                break;
        }

        Sprite sp = Resources.Load<Sprite>(path);
        if (sp == null)
            Debug.LogError($"Load image {resName} failed!");
        return sp;
    }

    public static GameObject LoadPrefab(EPrefabType pType, string resName)
    {
        string path = "";
        switch (pType)
        {
            case EPrefabType.Wnd:
                path = Path.Combine(prefabPath, "Wnd", resName);
                break;
            case EPrefabType.Unit:
                path = Path.Combine(prefabPath, "Unit", resName);
                break;
        }

        GameObject go = Resources.Load<GameObject>(path);
        if (go == null)
            Debug.LogError($"Load Prefab {resName} failed!");
        return go;
    }
}
