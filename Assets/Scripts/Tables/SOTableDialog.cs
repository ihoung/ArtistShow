using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERoleImgPos
{
    None = 0,
    Left,
    Middle,
    Right,
}

[System.Serializable]
public class SOTableItemDialog : SOTableItem
{
    public string name;
    public string dialog_content;
    public string role_img;
    public ERoleImgPos role_img_pos;
    public string bg_img;
    public int next_id;
}

[System.Serializable]
public class SOTableDialog : SOTable<SOTableItemDialog>
{

}
