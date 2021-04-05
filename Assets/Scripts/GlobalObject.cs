using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        TableMgr.Instance.Init();
        UIMgr.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
