using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicWndMono : MonoBehaviour
{
    protected abstract void OnInit();

    private void Awake()
    {
        OnInit();
    }
}
