using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIText : MonoBehaviour
{
    public string UIKey;

    private Text m_txt;

    void Awake()
    {
        m_txt = transform.GetComponent<Text>();
    }

    void Start()
    {
        var data = TableMgr.Instance.TableUIString.GetFirstItem(item => item.Key == UIKey);
        if (data != null)
        {
            m_txt.text = data.Value;
        }
        else
        {
            m_txt.text = $"Invalid UI key: {UIKey}";
        }
    }
}
