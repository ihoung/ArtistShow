using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemDialog : TableItem<SOTableItemDialog>
{
    public string Name { get; private set; }
    public string DialogContent { get; private set; }
    public List<string> RoleImg { get; private set; }
    public ERoleImgPos RoleImgPos { get; private set; }
    public string BgImg { get; private set; }
    public int NextID { get; private set; }
    public List<int> Options { get; private set; }

    public override void Parse(SOTableItemDialog SOData)
    {
        Name = SOData.name;
        DialogContent = SOData.dialog_content;
        RoleImg = ParseUtil.ParseStrList(SOData.role_img);
        RoleImgPos = SOData.role_img_pos;
        BgImg = SOData.bg_img;
        NextID = SOData.next_id;
        Options = ParseUtil.ParseIntList(SOData.options);
    }
}
