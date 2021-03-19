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
        UILayerMgr.Instance.Init();
        TableMgr.Instance.Init();

        DialogWnd.Instance.ShowDialog(1001);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
