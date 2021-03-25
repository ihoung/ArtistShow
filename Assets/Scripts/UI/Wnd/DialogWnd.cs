using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogWnd : BaseSingleTonWnd<DialogWnd, DialogWndMono>
{
    protected override string ResName => "DialogWnd";

    protected override EUILayer Layer => EUILayer.Top;

    protected override void OnInit()
    {
        Mono.btnNext.onClick.AddListener(OnClickNext);
    }

    private TableItemDialog m_curDialog;

    protected override void OnShow()
    {
        RefreshDialog();
    }

    protected override void OnHide()
    {
        m_curDialog = default;
    }

    protected override void OnDestroy()
    {

    }

    public void ShowDialog(int dialogID)
    {
        m_curDialog = TableMgr.Instance.TableDialog.GetItem(dialogID);

        if (m_curDialog == null)
            Debug.LogError($"Cannot find the target dialog! ID: {dialogID}");

        Show();
    }

    private void RefreshDialog()
    {
        if (m_curDialog == null)
            return;

        if (string.IsNullOrEmpty(m_curDialog.BgImg))
        {
            Mono.imgBg.gameObject.SetActive(false);
        }
        else
        {
            Mono.imgBg.gameObject.SetActive(true);
            Mono.imgBg.sprite = ResUtil.LoadSprite(ESpriteType.Bg, m_curDialog.BgImg);
        }

        Mono.txtName.SetText(m_curDialog.Name);
        Mono.txtDialog.SetText(m_curDialog.DialogContent);

        Mono.imgRoleLeft.gameObject.SetActive(m_curDialog.RoleImgPos == ERoleImgPos.Left);
        Mono.imgRoleMiddle.gameObject.SetActive(m_curDialog.RoleImgPos == ERoleImgPos.Middle);
        Mono.imgRoleRight.gameObject.SetActive(m_curDialog.RoleImgPos == ERoleImgPos.Right);
        switch (m_curDialog.RoleImgPos)
        {
            case ERoleImgPos.Left:
                Mono.imgRoleLeft.SetSprite(ESpriteType.Character, m_curDialog.RoleImg);
                break;
            case ERoleImgPos.Middle:
                Mono.imgRoleMiddle.SetSprite(ESpriteType.Character, m_curDialog.RoleImg);
                break;
            case ERoleImgPos.Right:
                Mono.imgRoleRight.SetSprite(ESpriteType.Character, m_curDialog.RoleImg);
                break;
            default:
                break;
        }
    }

    private void OnClickNext()
    {
        if (m_curDialog.NextID == 0)
        {
            Hide();
        }
        else
        {
            m_curDialog = TableMgr.Instance.TableDialog.GetItem(m_curDialog.NextID);
            RefreshDialog();
        }
    }
}
