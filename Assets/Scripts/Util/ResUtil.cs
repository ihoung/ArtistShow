using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ResUtil
{
    private static readonly string soPath = "Tables/";

    public static TSO LoadSO<TSO>(string resName) where TSO : ScriptableObject
    {
        TSO so = Resources.Load<TSO>(soPath + resName);
        if (so == null)
            Debug.LogError($"Load {resName} failed!");
        return so;
    }
}
